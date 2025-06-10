using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;

    private StatusUIItemSlot[] _itemSlots;

    private void Awake()
    {
        _itemSlots = GetComponentsInChildren<StatusUIItemSlot>();
        
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Init(OnSelectedItemSlot);
        }
    }

    private void OnSelectedItemSlot(StatusUIItemSlot slot)
    {
        inventoryUI.Show(slot.ItemType);
    }
}
