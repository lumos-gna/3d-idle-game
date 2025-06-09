using System;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    private readonly Dictionary<StatType, float> _statDict = new();

    public void Init(StatData[] statDataList)
    {
        foreach (var stat in statDataList)
        {
            _statDict[stat.type] = stat.value;
        }
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
