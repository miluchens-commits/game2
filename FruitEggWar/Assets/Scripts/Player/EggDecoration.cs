using UnityEngine;
using System.Collections.Generic;

public class EggDecoration : MonoBehaviour
{
    [Header("Decoration Slots")]
    public Transform headSlot;
    public Transform bodySlot;

    private Dictionary<DecorationCategory, DecorationItem> equippedItems = new Dictionary<DecorationCategory, DecorationItem>();
    private List<DecorationItem> ownedItems = new List<DecorationItem>();

    public bool EquipDecoration(DecorationItem item)
    {
        if (!item.isOwned) return false;

        if (equippedItems.ContainsKey(item.category))
        {
            UnequipDecoration(item.category);
        }

        GameObject instance = Instantiate(item.visualPrefab, GetSlotForCategory(item.category));
        equippedItems[item.category] = item;
        item.isEquipped = true;
        return true;
    }

    public void UnequipDecoration(DecorationCategory category)
    {
        if (equippedItems.ContainsKey(category))
        {
            Transform slot = GetSlotForCategory(category);
            for (int i = 0; i < slot.childCount; i++)
            {
                Destroy(slot.GetChild(i).gameObject);
            }
            equippedItems[category].isEquipped = false;
            equippedItems.Remove(category);
        }
    }

    private Transform GetSlotForCategory(DecorationCategory category)
    {
        return category == DecorationCategory.Head ? headSlot : bodySlot;
    }

    public void AddOwnedItem(DecorationItem item)
    {
        if (!ownedItems.Contains(item))
        {
            ownedItems.Add(item);
            item.isOwned = true;
        }
    }

    public List<DecorationItem> GetOwnedItems() => ownedItems;
}
