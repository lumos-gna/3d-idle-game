using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryHandler inventoryHandler;

    [SerializeField] private InventoryUISlot slotPrefab;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private GameObject noItemsPannel;

    private List<InventoryUISlot> _createdSlots = new();

    public void Show(ItemType itemType)
    {
        gameObject.SetActive(true);

        InitSlots(itemType);
        
    }

    private void InitSlots(ItemType itemType)   
    {
        List<ItemData> itemDatas = inventoryHandler.GetUnequipItemDatas(itemType);

        if (itemDatas == null)
        {
            noItemsPannel.gameObject.SetActive(true);
            return;
        }

        for (int i = 0; i < itemDatas.Count; i++)
        {
            if (_createdSlots.Count > i)
            {
                _createdSlots[i].gameObject.SetActive(true);
            }
            else
            {
                InventoryUISlot createdSlot =  Instantiate(slotPrefab, slotsParent);
                
                _createdSlots.Add(createdSlot);
            }
            
            _createdSlots[i].Init(itemDatas[i]);
        }

        for (int i = itemDatas.Count; i < _createdSlots.Count; i++)
        {
            _createdSlots[i].gameObject.SetActive(false);
        }
    }
    

}
