using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIItemInfo : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private Button equippedButton;
    [SerializeField] private TextMeshProUGUI equippedButtonText;

    private InventoryUI _inventoryUI;

    private void Awake()
    {
        _inventoryUI = GetComponentInParent<InventoryUI>();
    }


    public void InitItemInfo(ItemData itemData ,bool isEquipped)
    {
        icon.sprite = itemData.Icon;
        nameText.text = itemData.Name;
        equippedButtonText.text = isEquipped ? "Equip" : "UnEquip";

    }

    
}
