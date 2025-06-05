using System;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private StatData statData;

    private readonly Dictionary<StatType, float> _statDict = new();

    private void Awake()
    {
        InitializeStat();
    }

    void InitializeStat()
    {
        /*foreach (StatEntry entry in statData.Stats)
        {
            _statDict[entry.type] = entry.value;
        }*/
    }
    
    public float GetStat(StatType statType)
    {
        return _statDict.ContainsKey(statType) ? _statDict[statType] : 0;
    }

    public void ModifyStat(StatType statType, float amount)
    {
        if (!_statDict.ContainsKey(statType)) return;

        _statDict[statType] += amount;
    }
    
    
}
