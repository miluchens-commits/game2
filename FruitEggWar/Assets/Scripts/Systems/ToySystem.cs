using UnityEngine;

public class ToySystem : MonoBehaviour
{
    [System.Serializable]
    public class Toy
    {
        public string toyName;
        public int toyId;
        public GameObject modelPrefab;
        public bool isOwned;
        public int priceFruitCoins;
    }

    public Toy[] availableToys;
    public Toy[] ownedToys;

    public bool BuyToy(int toyId)
    {
        foreach (var toy in availableToys)
        {
            if (toy.toyId == toyId && !toy.isOwned)
            {
                if (CurrencyManager.Instance != null &&
                    CurrencyManager.Instance.SpendCurrency(CurrencyType.FruitCoin, toy.priceFruitCoins))
                {
                    toy.isOwned = true;
                    return true;
                }
            }
        }
        return false;
    }

    public void DisplayToy(int toyId)
    {
        foreach (var toy in availableToys)
        {
            if (toy.toyId == toyId && toy.isOwned && toy.modelPrefab != null)
            {
                Instantiate(toy.modelPrefab, Vector3.zero, Quaternion.identity);
                break;
            }
        }
    }
}
