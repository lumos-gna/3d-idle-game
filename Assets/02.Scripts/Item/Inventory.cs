using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Inventory
{
    public int maxSlots = 20;
    public List<Item> items = new();

    public event UnityAction<Item> OnAddItem;
    public event UnityAction<Item> OnRemoveitem;

    
    public void AddItem(ItemData itemData, int amount, out int remainAmount)
    {
        var targetSlots = items.FindAll((slot) => slot.itemData == itemData);

        targetSlots = targetSlots.OrderByDescending(slot => slot.amount).ToList();

        foreach (var slot in targetSlots)
        {
            if (slot.amount < itemData.MaxStack)
            {
                int addSpace = itemData.MaxStack - slot.amount;
                
                int addAmount = Mathf.Min(addSpace, amount);
                
                slot.amount += addAmount;
                
                amount -= addAmount;
                
                OnAddItem?.Invoke(slot);
            }
        }
        
        while (amount > 0 && items.Count < maxSlots)
        {
            int addAmount = Mathf.Min(itemData.MaxStack, amount);
            
            var newSlot = new Item(itemData, addAmount);

            items.Add(newSlot);
            
            amount -= addAmount;

            OnAddItem?.Invoke(newSlot);
        }
        
        remainAmount = amount;
    }

    public void AddItemToUnlimit(ItemData itemData, int amount)
    {
        var targetSlots = items.FindAll((slot) => slot.itemData == itemData);

        targetSlots = targetSlots.OrderByDescending(slot => slot.amount).ToList();

        foreach (var slot in targetSlots)
        {
            if (slot.amount < itemData.MaxStack)
            {
                int addSpace = itemData.MaxStack - slot.amount;
                
                int addAmount = Mathf.Min(addSpace, amount);
                
                slot.amount += addAmount;
                
                amount -= addAmount;
                
                OnAddItem?.Invoke(slot);
            }
        }
        
        while (amount > 0)
        {
            int addAmount = Mathf.Min(itemData.MaxStack, amount);
            
            var newSlot = new Item(itemData, addAmount);

            items.Add(newSlot);
            
            amount -= addAmount;

            OnAddItem?.Invoke(newSlot);
        }
    }
    
   

    public void RemoveItem(Item targetSlot, int amount)
    {
        if (targetSlot == null || !items.Contains(targetSlot))
            return;

        int removeAmount = Mathf.Min(targetSlot.amount, amount);
        
        targetSlot.amount -= removeAmount;
        
        OnRemoveitem?.Invoke(targetSlot);

        if (targetSlot.amount == 0)
        {
            items.Remove(targetSlot);
        }
    }
}