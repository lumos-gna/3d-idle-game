using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void SpawnEnemies(Stage stage)
    {
        var targetEnemyDatas 
            = GameManager.Instance.GameData.GetEnemies((data) => data.Level == stage.Level);

        if (targetEnemyDatas.Count == 0)
        {
            Debug.LogError("잘못된 적 데이터");
            return;
        }
        
        for (int roomIndex = 0; roomIndex < stage.Rooms.Count; roomIndex++)
        {
            if (roomIndex == 0)
                continue;
            
            Room targetRoom = stage.Rooms[roomIndex];

            for (int enemyCount = 0; enemyCount < stage.MaxEnemyCount; enemyCount++)
            {
                EnemyData randEnemyData = targetEnemyDatas[Random.Range(0, targetEnemyDatas.Count)];

                Enemy targetEnemy = Instantiate(randEnemyData.Prefab);
                    
                targetRoom.Enemies.Add(targetEnemy);
            }
            
            SetSpawnEnemyPos(targetRoom);
        }

    }

    private void SetSpawnEnemyPos(Room room)
    {
        List<Vector2Int> spawnedPosList = new();
        
        while (spawnedPosList.Count < room.Enemies.Count)
        {
            Vector2Int spawnPos 
                = new Vector2Int(
                    Random.Range(room.Size.x/2 * -1 + 1, room.Size.x/2 - 1), 
                    Random.Range(room.Size.y/2 * -1 + 1, room.Size.y/2 - 1));
            
            if (!spawnedPosList.Contains(spawnPos))
            {
                spawnedPosList.Add(spawnPos);
            }
        }

        for (int i = 0; i < room.Enemies.Count; i++)
        {
            room.Enemies[i].transform.position 
                = room.transform.position + new Vector3(spawnedPosList[i].x, 1, spawnedPosList[i].y);
        }
        
    }
}
