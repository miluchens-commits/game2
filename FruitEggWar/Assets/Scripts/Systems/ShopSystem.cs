using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public int itemId;
        public CurrencyType currencyType;
        public int price;
        public DecorationCategory category;
        public GameObject visualPrefab;
        public Sprite icon;
    }

    public ShopItem[] clothingItems;
    public ShopItem[] accessoryItems;
    public ShopItem[] souvenirItems;

    public bool PurchaseItem(ShopItem item)
    {
        if (CurrencyManager.Instance != null &&
            CurrencyManager.Instance.SpendCurrency(item.currencyType, item.price))
        {
            Debug.Log($"Purchased: {item.itemName}");
            return true;
        }
        return false;
    }

    public ShopItem[] GetClothingItems() => clothingItems;
    public ShopItem[] GetAccessoryItems() => accessoryItems;
    public ShopItem[] GetSouvenirItems() => souvenirItems;
}
