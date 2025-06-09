using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    public ItemData ItemData { get; private set; }

    public void Init(ItemData itemData)
    {
        ItemData = itemData;
    }
}