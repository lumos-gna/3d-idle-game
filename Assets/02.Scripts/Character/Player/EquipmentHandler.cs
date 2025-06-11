using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentHandler : MonoBehaviour
{
    public Dictionary<ItemType, Item> equipItems = new();

    public event UnityAction<Item> OnEquip;
    public event UnityAction<Item> OnUnequip;

    public void Equip(Item item)
    {
        ItemType targetType = item.itemData.Type;
        
        Unequip(targetType);

        equipItems[targetType] = item;
        
        OnEquip?.Invoke(item);
    }

    public void Unequip(ItemType itemType)
    {
        if (equipItems.ContainsKey(itemType))
        {
            OnUnequip?.Invoke(equipItems[itemType]);

            equipItems[itemType] = null;
        }
    }

    public Item GetItem(ItemType itemType)
    {
        if (equipItems.ContainsKey(itemType))
        {
            return equipItems[itemType];
        }

        return null;
    }
}