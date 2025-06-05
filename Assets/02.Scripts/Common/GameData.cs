using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Data/Game", order = 0)]
public class GameData : ScriptableObject
{
    [field: SerializeField] public List<StageData> StageDatas { get; private set; }

    [field: SerializeField] public List<EnemyData> EnemyDatas { get; private set; }


    public StageData GetStage(Func<StageData, bool> condition)
    {
       return StageDatas.Find((data) => condition(data));
    }

    public List<EnemyData> GetEnemies(Func<EnemyData, bool> condition)
    {
        List<EnemyData> enemies = new();
        
        for (int index = 0; index < EnemyDatas.Count; index++)
        {
            if (condition(EnemyDatas[index]))
            {
                enemies.Add(EnemyDatas[index]);
            }
        }

        return enemies;
    }
}
