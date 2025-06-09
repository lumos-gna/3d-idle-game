using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public StageData StageData { get; private set; }

    private List<Room> _roomList = new();


    public void Init(StageData stageData, List<EnemyData> enemyDataList, List<Vector2Int> roomCells)
    {
        StageData = stageData;

        CreateRooms(stageData, roomCells);
        
        StartCoroutine(DelayToCreate(enemyDataList));
    }

    IEnumerator DelayToCreate(List<EnemyData> enemyDataList)
    {
        yield return null;
                
        GetComponent<NavMeshSurface>().BuildNavMesh();
        
        InitRooms(enemyDataList);

        GameManager.Instance.Player.StartStage();
    }

    public Room GetNextRoom(Room currentRoom)
    {
        for (int i = 0; i < _roomList.Count; i++)
        {
            if (_roomList[i] == currentRoom)
            {
                if (i < _roomList.Count - 1)
                {
                    return _roomList[i + 1];
                }
            }
        }
        
        return null;
    }

    private void CreateRooms(StageData stageData, List<Vector2Int> roomCells)
    {
        StageData = stageData;
        
        for (int i = 0; i < roomCells.Count; i++)
        {
            Room createRoom = Instantiate(stageData.RoomPrefab, transform);
            
            createRoom.transform.position =
                new Vector3(roomCells[i].x, 0, roomCells[i].y) * (stageData.RoomSize.x + stageData.PathSize.x);
            
            _roomList.Add(createRoom);

            //Path
            if (i < roomCells.Count - 1)
            {
                var createPath = Instantiate(stageData.PathPrefab, transform);
         
                Vector2 centerPos = (Vector2)(roomCells[i + 1] + roomCells[i])  * 0.5f;

                createPath.transform.position = new Vector3(centerPos.x, 0, centerPos.y) * (stageData.RoomSize.x + stageData.PathSize.x);
         
                Vector2 delta = roomCells[i + 1] - roomCells[i];
         
                if (delta.y != 0) 
                {
                    createPath.transform.rotation = Quaternion.Euler(0, 90f, 0);
                }
            }
        }
    }
    
    private void InitRooms(List<EnemyData> enemyDataList)
    {
        for (int i = 0; i < _roomList.Count; i++)
        {
            List<EnemyData> roomEnemyData = null;
            
            if (i > 0)
            {
                roomEnemyData = i < _roomList.Count - 1 ? 
                    GetRoomEnemyDataList(false, enemyDataList) :
                    GetRoomEnemyDataList(true, enemyDataList);
            }

            _roomList[i].Init(this, roomEnemyData);
        }
    }
    
    private List<EnemyData> GetRoomEnemyDataList(bool isBoss, List<EnemyData> enemyDataList)
    {
        List<EnemyData> targetEnemytDatas = new();

        for (int i = 0; i < enemyDataList.Count; i++)
        {
            if (enemyDataList[i].IsBoss == isBoss)
            {
                targetEnemytDatas.Add(enemyDataList[i]);
            }
        }

        return targetEnemytDatas;
    }
    
   

}