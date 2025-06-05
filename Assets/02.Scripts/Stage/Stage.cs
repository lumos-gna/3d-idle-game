using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public int Level { get; private set; }
    public int MaxEnemyCount { get; private set; }
    public List<Room> Rooms { get; private set; }
    
    public Stage(StageData stageData,  List<Room> rooms)
    {
        Level = stageData.Level;
        MaxEnemyCount = stageData.MaxEnemyCount;
        
        Rooms = rooms;
        
        for (int i = 0; i < Rooms.Count; i++)
        {
            Rooms[i].Initialize(i, stageData.RoomSize);
        }
        
    }
}