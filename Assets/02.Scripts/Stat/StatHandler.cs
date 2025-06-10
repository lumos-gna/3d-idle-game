using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    public Dictionary<StatType, Stat> Stats => _stats;
    
    private Dictionary<StatType, Stat> _stats = new();


    public void Init(StatData[] statDatas)
    {
        _stats = new();
        
        foreach (var statData in statDatas)
        {
            _stats[statData.type] = new(statData);
        }
    }
    
    public bool TryGetStat(StatType statType, out Stat stat)
    {
        stat = _stats.ContainsKey(statType)  ? _stats[statType] : null;
        
        return stat != null;
    }
}
