using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Data/Game", order = 0)]
public class GameData : ScriptableObject
{
    [field: SerializeField] public List<StageData> StageDatas { get; private set; }
    [field: SerializeField] public List<EnemyData> EnemyDatas { get; private set; }
}
