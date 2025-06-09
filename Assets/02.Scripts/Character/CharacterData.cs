using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Character Data/Base")]
public class CharacterData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    
    [field: Space(10f)]
    [field: SerializeField] public StatData[] Stats { get; private set; }
}

