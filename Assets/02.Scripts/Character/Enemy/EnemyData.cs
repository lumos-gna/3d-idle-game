using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/Character Data/Enemy")]
public class EnemyData : CharacterData
{
    [field: Space(20f)]
    [field: Header("Enemy")]
    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public int DropGold { get; private set; }
    [field: SerializeField] public bool IsBoss { get; private set; }
    
    
    [field: Space(10f)]
    [field: SerializeField] public Enemy Prefab { get; private set; }
}
