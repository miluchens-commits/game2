using UnityEngine;
using System.Collections.Generic;

public class GachaSystem : MonoBehaviour
{
    public static GachaSystem Instance { get; private set; }

    [System.Serializable]
    public class GachaItem
    {
        public string itemName;
        public int itemId;
        public Rarity rarity;
        public GameObject itemPrefab;
        public Sprite itemIcon;
    }

    [System.Serializable]
    public class GachaPool
    {
        public string poolName;
        public CurrencyType costCurrency;
        public int costAmount;
        public List<GachaItem> items;
    }

    public GachaPool fruitCoinPool;
    public GachaPool sparkleCoinPool;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GachaItem PullGacha(GachaPool pool)
    {
        if (!CurrencyManager.Instance.SpendCurrency(pool.costCurrency, pool.costAmount))
            return null;

        float roll = Random.Range(0f, 100f);
        List<GachaItem> eligibleItems = new List<GachaItem>();

        foreach (var item in pool.items)
        {
            float chance = GetRarityChance(item.rarity);
            if (roll < chance)
            {
                eligibleItems.Add(item);
                break;
            }
            roll -= chance;
        }

        if (eligibleItems.Count > 0)
        {
            return eligibleItems[Random.Range(0, eligibleItems.Count)];
        }

        return pool.items[0];
    }

    private float GetRarityChance(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common: return 50f;
            case Rarity.Rare: return 30f;
            case Rarity.Epic: return 15f;
            case Rarity.Legendary: return 5f;
            default: return 50f;
        }
    }

    public GachaItem PullFruitCoinGacha()
    {
        return PullGacha(fruitCoinPool);
    }

    public GachaItem PullSparkleCoinGacha()
    {
        return PullGacha(sparkleCoinPool);
    }
}
