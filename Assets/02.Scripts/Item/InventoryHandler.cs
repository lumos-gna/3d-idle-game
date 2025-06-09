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
  
}