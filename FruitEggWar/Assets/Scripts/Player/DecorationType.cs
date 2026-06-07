using UnityEngine;

public enum DecorationCategory
{
    Head,
    Body
}

public enum HeadDecorationType
{
    Hair,
    Hat,
    Crown
}

public enum BodyDecorationType
{
    Clothes,
    Pants,
    Backpack,
    ChestHang
}

[System.Serializable]
public class DecorationItem
{
    public string itemName;
    public int itemId;
    public DecorationCategory category;
    public Sprite previewIcon;
    public GameObject visualPrefab;
    public bool isOwned;
    public bool isEquipped;
    public Rarity rarity;
}

public enum Rarity
{
    Common,
    Rare,
    Epic,
    Legendary
}
