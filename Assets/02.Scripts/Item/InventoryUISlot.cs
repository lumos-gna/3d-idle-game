using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    public Item Item => _item;

    [SerializeField] private Image icon;
    [SerializeField] private Image equippedImage;
    [SerializeField] private Image selectedImage;
    
    [SerializeField] private Button button;
    
    private Item _item;

    public void Init(InventoryUI inventoryUI, Item item)
    {
        _item = item;
        
        icon.sprite = item.itemData.Icon;
        
        button.onClick.AddListener(() => inventoryUI.SelectSlot(this));


        ToggleEquippedImage(false);
        ToggleSelectedImage(false);
    }
    
    public void ToggleEquippedImage(bool isEnable)
        => equippedImage.enabled = isEnable;

    public void ToggleSelectedImage(bool isEnable)
    {
        selectedImage.enabled = isEnable;
    }
}
