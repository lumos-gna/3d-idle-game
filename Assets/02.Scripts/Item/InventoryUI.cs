using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    public InventoryUISlot SelectedSlot { get; private set; }
    public InventoryUISlot EquippedSlot { get; private set; }
    
    [SerializeField] private Inventory inventory;
    
    [SerializeField] private InventoryUIItemInfo inventoryItemInfo;

    [SerializeField] private InventoryUISlot slotPrefab;
    
    [SerializeField] private Transform slotsParent;
    
    [SerializeField] private GameObject noItemsPannel;
    
    
    public void Show(ItemType itemType)
    {
        gameObject.SetActive(true);

        InitSlots(itemType);
    }
    

    private void InitSlots(ItemType itemType)   
    {
        List<Item> items = inventory.GetItems(itemType);

        if (items == null)
        {
            noItemsPannel.gameObject.SetActive(true);
            return;
        }

        for (int i = 0; i < slotsParent.childCount; i++)
        {
            Destroy(slotsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < items.Count; i++)
        {
            InventoryUISlot createdSlot =  Instantiate(slotPrefab, slotsParent);
            
            createdSlot.Init(this, items[i]);
            
            var equippedItem = inventory.GetEquippedItem(itemType);

            if (equippedItem != null)
            {
                inventoryItemInfo.InitItemInfo(equippedItem, true);
            }
            
            if (equippedItem == createdSlot.Item)
            {
                SelectSlot(createdSlot);

                EquipSelectedSlotItem();
            }
        }
    }
    

    public void SelectSlot(InventoryUISlot slot)
    {
        if (SelectedSlot != null)
        {
            SelectedSlot.ToggleSelectedImage(false);
        }
        
        SelectedSlot = slot;
        
        SelectedSlot.ToggleSelectedImage(true);
        
        
        var equippedItem = inventory.GetEquippedItem(slot.Item.itemData.Type);
            
        if (equippedItem == slot.Item)
        {
            inventoryItemInfo.InitItemInfo(slot.Item, true);
        }
        else
        {
            inventoryItemInfo.InitItemInfo(slot.Item, false);
        }
    }
    

    public void EquipSelectedSlotItem()
    {
        if (EquippedSlot != null)
        {
            EquippedSlot.ToggleEquippedImage(false);
        }
        
        EquippedSlot = SelectedSlot;
        
        EquippedSlot.ToggleEquippedImage(true);
        
        
        inventory.EquipItem(EquippedSlot.Item);
        
        inventoryItemInfo.InitItemInfo(EquippedSlot.Item, true);
    }
    

    public void SellSelectedSlotItem()
    {
        SelectSlot(EquippedSlot);
        
        SelectedSlot = EquippedSlot;
        
        inventory.SellItem(SelectedSlot.Item);
        
        Destroy(SelectedSlot.gameObject);
    }
}
