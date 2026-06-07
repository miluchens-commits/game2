using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    public CurrencyData playerCurrency = new CurrencyData();

    [Header("Starting Currency")]
    public int startingFruitCoins = 100;
    public int startingSparkleCoins = 10;

    [Header("Reward Amounts")]
    public int loginRewardFruitCoins = 50;
    public int taskRewardFruitCoins = 30;
    public int eventRewardFruitCoins = 100;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeCurrency();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeCurrency()
    {
        playerCurrency.AddFruitCoins(startingFruitCoins);
        playerCurrency.AddSparkleCoins(startingSparkleCoins);
    }

    public void AddCurrency(CurrencyType type, int amount)
    {
        switch (type)
        {
            case CurrencyType.FruitCoin:
                playerCurrency.AddFruitCoins(amount);
                break;
            case CurrencyType.SparkleCoin:
                playerCurrency.AddSparkleCoins(amount);
                break;
        }
    }

    public bool SpendCurrency(CurrencyType type, int amount)
    {
        bool hasEnough = type == CurrencyType.FruitCoin
            ? playerCurrency.HasEnoughFruitCoins(amount)
            : playerCurrency.HasEnoughSparkleCoins(amount);

        if (hasEnough)
        {
            switch (type)
            {
                case CurrencyType.FruitCoin:
                    playerCurrency.SpendFruitCoins(amount);
                    break;
                case CurrencyType.SparkleCoin:
                    playerCurrency.SpendSparkleCoins(amount);
                    break;
            }
            return true;
        }
        return false;
    }

    public int GetCurrency(CurrencyType type)
    {
        return type == CurrencyType.FruitCoin
            ? playerCurrency.fruitCoins
            : playerCurrency.sparkleCoins;
    }

    public void RewardDailyLogin()
    {
        AddCurrency(CurrencyType.FruitCoin, loginRewardFruitCoins);
    }

    public void RewardTaskComplete()
    {
        AddCurrency(CurrencyType.FruitCoin, taskRewardFruitCoins);
    }

    public void RewardEvent()
    {
        AddCurrency(CurrencyType.FruitCoin, eventRewardFruitCoins);
    }
}
