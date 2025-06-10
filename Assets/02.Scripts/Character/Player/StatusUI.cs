using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private InventoryUI inventoryUI;

    [SerializeField] private StatHandler statHandler;
    
    [SerializeField] private TextMeshProUGUI statText;
    
    private StatusUIItemSlot[] _itemSlots;


    private void Awake()
    {
        _itemSlots = GetComponentsInChildren<StatusUIItemSlot>();
        
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].Init(OnSelectedItemSlot);
        }
    }


    public void Show()
    {
        gameObject.SetActive(true);

        statText.text = "";
        
        foreach (var stat in  statHandler.Stats)
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
