using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIItemInfo : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI statText;

    [SerializeField] private Button equippedButton;
    [SerializeField] private Button sellButton;

    private void Awake()
    {
        InventoryUI inventoryUI = GetComponentInParent<InventoryUI>();
        
        equippedButton.onClick.AddListener(inventoryUI.EquipSelectedSlotItem);
        
        sellButton.onClick.AddListener(inventoryUI.SellSelectedSlotItem);
    }


    public void InitItemInfo(Item item ,bool isEquipped)
    {
        icon.sprite = item.itemData.Icon;
        nameText.text = item.itemData.Name;

        statText.text = "";
        
        for (int i = 0; i < item.itemData.StatDatas.Length; i++)
        {
            statText.text += $"{item.itemData.StatDatas[i].type} : {item.itemData.StatDatas[i].baseValue} \n";
        }        
        
        equippedButton.gameObject.SetActive(!isEquipped);
        sellButton.gameObject.SetActive(!isEquipped);
    }
}
