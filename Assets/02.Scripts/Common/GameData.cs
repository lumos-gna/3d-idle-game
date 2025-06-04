using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Data/Game", order = 0)]
public class GameData : ScriptableObject
{
    [field: SerializeField] private List<StageData> StageDatas;


    public StageData GetStage(Func<StageData, bool> condition)
    {
       return StageDatas.Find((data) => condition(data));
    }
}
