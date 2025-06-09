using UnityEngine;


[CreateAssetMenu(fileName = "PlayerBaseData", menuName = "Scriptable Objects/Data/Player")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public StatData[] StatDatas { get; private set; }
}
