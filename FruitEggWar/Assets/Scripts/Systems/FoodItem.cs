using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public enum FoodType
    {
        Ramen,
        IceCream,
        Juice
    }

    public FoodType foodType;
    public int price = 10;
    public CurrencyType currencyType = CurrencyType.FruitCoin;
    public GameObject foodModel;

    public bool Buy()
    {
        if (CurrencyManager.Instance != null &&
            CurrencyManager.Instance.SpendCurrency(currencyType, price))
        {
            if (foodModel != null)
            {
                Instantiate(foodModel, transform.position, Quaternion.identity);
            }
            return true;
        }
        return false;
    }
}
