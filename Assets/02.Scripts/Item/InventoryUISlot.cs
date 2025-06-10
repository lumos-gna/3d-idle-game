using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    public ItemData ItemData { get; private set; }

    [SerializeField] private Image icon;

    public void Init(ItemData itemData)
    {
        ItemData = itemData;
        
        icon.sprite = itemData.Icon;
    }
}
