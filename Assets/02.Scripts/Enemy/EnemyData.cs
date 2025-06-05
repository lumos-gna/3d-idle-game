using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    
    [field: Space(10f)]
    [field: SerializeField] public int Level { get; private set; }
    
    [field: Space(10f)]
    [field: SerializeField] public StatData[] StatDatas { get; private set; }
    
    [field: Space(10f)]
    [field: SerializeField] public Enemy Prefab { get; private set; }
}
