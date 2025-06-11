using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public InventoryUISlot SelectedSlot { get; private set; }
    public InventoryUISlot EquippedSlot { get; private set; }
    
    
    [SerializeField] private InventoryUIItemInfo inventoryItemInfo;

    [SerializeField] private InventoryUISlot slotPrefab;
    
    [SerializeField] private Transform slotsParent;
    
    [SerializeField] private GameObject noItemsPannel;

    [SerializeField] private PlayerController player;


    public void Init()
    {
        player.Inventory.OnAddItem += (item) =>
        {
            if (gameObject.activeInHierarchy)
            {
                InitSlots(item.itemData.Type);
            }
        };
    }

    public void Show(ItemType itemType)
    {
        gameObject.SetActive(true);

        InitSlots(itemType);
    }

    private void InitSlots(ItemType itemType)
    {
        List<Item> targetItems = player.Inventory.items.FindAll((item) => item.itemData.Type == itemType);
        
        if (targetItems.Count == 0)
        {
            noItemsPannel.gameObject.SetActive(true);
            return;
        }
        
        noItemsPannel.gameObject.SetActive(false);
        
        for (int i = 0; i < slotsParent.childCount; i++)
        {
            Destroy(slotsParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < targetItems.Count; i++)
        {
            InventoryUISlot createdSlot =  Instantiate(slotPrefab, slotsParent);
            
            createdSlot.Init(this, targetItems[i]);
            
            var equippedItem = player.EquipmentHandler.GetItem(itemType);

            if (equippedItem != null)
            {
                inventoryItemInfo.InitItemInfo(equippedItem, true);
                
                if (equippedItem == createdSlot.Item)
                {
                    SelectSlot(createdSlot);

                    EquipSelectedSlotItem();
                }
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
        
       
        var equippedItem = player.EquipmentHandler.GetItem(slot.Item.itemData.Type);
            
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
        
        
        player.EquipmentHandler.Equip(EquippedSlot.Item);
        
        inventoryItemInfo.InitItemInfo(EquippedSlot.Item, true);
    }
    

    public void SellSelectedSlotItem()
    {
        var targetItem = SelectedSlot.Item;
        
        GameManager.Instance.AddGold(targetItem.itemData.Price);
                
        player.Inventory.RemoveItem(targetItem, 1);
        
        Destroy(SelectedSlot.gameObject);
        
        
        SelectSlot(EquippedSlot);
    }
}
