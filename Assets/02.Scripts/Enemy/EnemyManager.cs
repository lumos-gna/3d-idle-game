using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void SpawnEnemies(Stage stage)
    {
        GameData gameData = GameManager.Instance.GameData;
        
        List<EnemyData> basicEnemyDatas 
            = gameData.GetEnemies((data) => data.Level == stage.Level && !data.IsBoss);
        
        List<EnemyData> bossEnemyDatas 
            = gameData.GetEnemies((data) => data.Level == stage.Level && data.IsBoss);

        if (basicEnemyDatas.Count == 0)
        {
            Debug.LogError("잘못된 적 데이터");
            return;
        }

        if (bossEnemyDatas.Count == 0)
        {
            Debug.LogError("잘못된 보스 데이터");
            return;
        }
        
        for (int roomIndex = 0; roomIndex < stage.Rooms.Count; roomIndex++)
        {
            if (roomIndex == 0)
                continue;
            
            Room targetRoom = stage.Rooms[roomIndex];
            
            if (roomIndex < stage.Rooms.Count - 1)
            {
                SpawnBasicEnemy(stage, targetRoom, basicEnemyDatas);
            }
            else
            {
                SpawnBossEnemy(stage, targetRoom, basicEnemyDatas);
            }
        }
    }

    private void SpawnBasicEnemy(Stage stage, Room room, List<EnemyData> basicEnemyDatas)
    {
        for (int enemyCount = 0; enemyCount < stage.MaxEnemyCount; enemyCount++)
        {
            EnemyData randEnemyData = basicEnemyDatas[Random.Range(0, basicEnemyDatas.Count)];

            Enemy targetEnemy = Instantiate(randEnemyData.Prefab);
                    
            room.Enemies.Add(targetEnemy);
        }
        
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

    private void SpawnBossEnemy(Stage stage, Room room, List<EnemyData> bossEnemyDatas)
    {
        EnemyData randEnemyData = bossEnemyDatas[Random.Range(0, bossEnemyDatas.Count)];

        Enemy targetEnemy = Instantiate(randEnemyData.Prefab);
                    
        room.Enemies.Add(targetEnemy);

        targetEnemy.transform.position = room.transform.position + new Vector3(0, 1, 0);
    }
}
