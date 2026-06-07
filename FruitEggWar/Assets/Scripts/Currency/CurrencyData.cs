using UnityEngine;

public enum CurrencyType
{
    FruitCoin,
    SparkleCoin
}

[System.Serializable]
public class CurrencyData
{
    public int fruitCoins = 0;
    public int sparkleCoins = 0;

    public void AddFruitCoins(int amount) { fruitCoins += amount; }
    public void SpendFruitCoins(int amount) { fruitCoins = Mathf.Max(0, fruitCoins - amount); }
    public void AddSparkleCoins(int amount) { sparkleCoins += amount; }
    public void SpendSparkleCoins(int amount) { sparkleCoins = Mathf.Max(0, sparkleCoins - amount); }
    public bool HasEnoughFruitCoins(int amount) => fruitCoins >= amount;
    public bool HasEnoughSparkleCoins(int amount) => sparkleCoins >= amount;
}
