using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatHandler : MonoBehaviour
{
    private Dictionary<StatType, Stat> _stats = new();
    
    public event UnityAction<Stat> OnStatModified;

    public void Init(StatData[] statDatas)
    {
        _stats = new();
        
        foreach (var statData in statDatas)
        {
            Stat targetStat = new(statData);

            targetStat.curValue = targetStat.maxValue;

            _stats[statData.type] = targetStat;
        }
    }
    
    public bool TryGetStat(StatType statType, out Stat stat)
    {
        stat =  _stats.ContainsKey(statType)  ? _stats[statType] : null;
        
        return stat != null;
    }

    public void ModifyStatMaxValue(StatType statType, float amount)
    {
        if (TryGetStat(statType, out Stat targetStat))
        {
            targetStat.maxValue += amount;

            OnStatModified?.Invoke(targetStat);
        }
    }
    
    public void ModifyStatCurValue(StatType statType, float amount)
    {
        if (TryGetStat(statType, out Stat targetStat))
        {
            targetStat.curValue += amount;

            OnStatModified?.Invoke(targetStat);
        }
    }
}
