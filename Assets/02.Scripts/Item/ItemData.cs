using UnityEngine;


[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/Data/Item")]
public class ItemData : ScriptableObject
{
    [field: SerializeField] public ItemType Type { get; private set; }

    [field: SerializeField] public string Name { get; private set; }
    
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public int MaxStack { get; private set; }
    
    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: SerializeField] public StatData[] StatDatas { get; private set; }
}
