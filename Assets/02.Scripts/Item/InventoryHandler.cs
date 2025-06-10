using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryHandler : MonoBehaviour
{
    private Dictionary<ItemType, List<ItemData>> _unequipItems = new();
    
    private Dictionary<ItemType, ItemData> _equipItems = new();
    
    public event UnityAction<ItemData> OnEquippedItem;
    public event UnityAction<ItemData> OnUnequippedItem;


    public void AddItem(ItemData itemData)
    {
        if (_unequipItems.ContainsKey(itemData.Type))
        {
            if (_unequipItems[itemData.Type] == null)
            {
                _unequipItems[itemData.Type] = new();
            }
        }
        else
        {
            _unequipItems[itemData.Type] = new();
        }
        
        _unequipItems[itemData.Type].Add(itemData);
    }
    
    
    public void RemoveItem(ItemData itemData)
    {
        if (_unequipItems.ContainsKey(itemData.Type))
        {
            _unequipItems[itemData.Type].Remove(itemData);
        }
    }
    

    public void EquipItem(ItemData equipItemData)
    {
        var preEquippedItem = _equipItems[equipItemData.Type];
        
        if (preEquippedItem != null)
        {
            OnUnequippedItem?.Invoke(preEquippedItem);
        }
        
        _equipItems[equipItemData.Type] = equipItemData;
        
        OnEquippedItem?.Invoke(equipItemData);
    }

    public List<ItemData> GetUnequipItemDatas(ItemType type)
    {
        if (_unequipItems.ContainsKey(type))
        {
            return _unequipItems[type];
        }

        return null;
    }

    public ItemData GetEquipItemData(ItemType type)
    {
        if (_equipItems.ContainsKey(type))
        {
            return _equipItems[type];
        }

        return null;
    }
}