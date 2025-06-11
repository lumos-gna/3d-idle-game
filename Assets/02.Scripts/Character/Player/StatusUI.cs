using System;
using TMPro;
using UnityEngine;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;

    [SerializeField] private PlayerController player;
    
    [SerializeField] private TextMeshProUGUI statText;
    
    private StatusUIItemSlot[] _itemSlots;

   
    public void Init()
    {
        _itemSlots = GetComponentsInChildren<StatusUIItemSlot>();
        
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Init(OnSelectedItemSlot);
        }

        player.EquipmentHandler.OnEquip += (item) =>
        {
            UpdateInfo();
        };
    }


    public void Show()
    {
        gameObject.SetActive(true);

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        statText.text = "";
        
        foreach (var stat in  player.StatHandler.Stats)
        {
            switch (stat.Key)
            {
                case StatType.MaxHealth :
                case StatType.Damage :
                    statText.text += $"{stat.Key.ToString()} : {stat.Value.value} \n";
                    break;
            }
        }
    }

    private void OnSelectedItemSlot(StatusUIItemSlot slot)
    {
        inventoryUI.Show(slot.ItemType);
    }
}
