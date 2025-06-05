using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public int Level { get; private set; }
    public List<Room> Rooms { get; private set; }
    public Room CurrentRoom { get; private set; }


    public Stage(StageData stageData,  List<Room> rooms)
    {
        if (rooms.Count == 0)
        {
            Debug.LogError("잘못된 방 개수");
            return;
        }
        
        Level = stageData.Level;
        
        Rooms = rooms;
        
        for (int i = 0; i < Rooms.Count; i++)
        {
            int maxEnemyCount = i == Rooms.Count - 1 ? 1 : stageData.MaxNormalEnemyCount;
            
            Rooms[i].Init(i, maxEnemyCount, stageData.RoomSize, RoomClear);
        }

        CurrentRoom = Rooms[0];
    }

    private void RoomClear()
    {
        if (Rooms.Count - 1 > CurrentRoom.RoomIndex)
        {
            CurrentRoom = Rooms[CurrentRoom.RoomIndex + 1];
        }
        else
        {
            Debug.Log("스테이지 클리어");
        }
    }

}