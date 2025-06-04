using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatData", menuName = "Scriptable Objects/Data/Stat")]
public class StatData : ScriptableObject
{
    public List<StatEntry> Stats => stats;

    [SerializeField] private List<StatEntry> stats;
}
