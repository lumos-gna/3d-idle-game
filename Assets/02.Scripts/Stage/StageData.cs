using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/Data/Stage")]
public class StageData : ScriptableObject
{
    [field: SerializeField] public int Level { get; private set; }
    
    
    [field: Space(10f)]
    [field: SerializeField] public int MaxRoomCount { get; private set; }
    
    
    [field: Space(10f)]
    [field: SerializeField] public Vector2Int RoomSize { get; private set; }
    [field: SerializeField] public Vector2 PathSize { get; private set; }
    
    
    [field: Space(20f)]
    [field: SerializeField] public Room RoomPrefab { get; private set; }
    
    [field: SerializeField] public GameObject PathPrefab { get; private set; }
    
    [field: Space(20f)] 
    [field: SerializeField] public StageEnemyInfo NormalEnemyInfos{ get; private set; }
    [field: SerializeField] public StageEnemyInfo BossEnemyInfos{ get; private set; }
    [field: SerializeField] public List<StageRewardInfo> RewardInfos{ get; private set; }
}