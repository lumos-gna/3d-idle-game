using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public void CreateStageEnemies(Stage stage)
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
                CreateRoomEnemies(targetRoom, basicEnemyDatas);
            }
            else
            {
                CreateRoomEnemies(targetRoom, bossEnemyDatas);
            }
        }
    }

    private void CreateRoomEnemies(Room room, List<EnemyData> targetEnemyDatas)
    {
        for (int enemyCount = 0; enemyCount < room.MaxEnemyCount; enemyCount++)
        {
            EnemyData randEnemyData = targetEnemyDatas[Random.Range(0, targetEnemyDatas.Count)];

            Enemy targetEnemy = Instantiate(randEnemyData.Prefab);
            
            targetEnemy.Init(randEnemyData);
                    
            room.AddEnemy(targetEnemy);
        }
    }
}
