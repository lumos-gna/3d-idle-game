using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public List<Enemy> Enemies => _enemyList;
    
    
    [SerializeField] private Collider triggerCollider;
    

    private Stage _stage;

    private List<Enemy> _enemyList = new();

    private PlayerController _player;



    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            _player = player;
            
            if (_enemyList.Count > 0)
            {
                for (int i = 0; i < _enemyList.Count; i++)
                {
                    _enemyList[i].Tracking(player);
                }
                
                _player.ChangeStage(PlayerState.Combat);
            }
            else
            {
                ClearRoom();
            }
        }
    }

    public void Init(Stage stage, List<EnemyData> enemyDataList)
    {
        _stage = stage;

        triggerCollider.enabled = true;

        if (enemyDataList == null || enemyDataList.Count == 0)
            return;

        CreateEnemies(enemyDataList);
    }
    
    public void OnEnemyDeath(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        
        if (_enemyList.Count == 0)
        {
            ClearRoom();
        }
    }

    private void ClearRoom()
    {
        var nextRoom = _stage.GetNextRoom(this);
        
        if (nextRoom == null)
        {
            GameManager.Instance.ClearedStage();
        }
        else
        {
            _player.CurrentRoom = nextRoom;
            _player.ChangeStage(PlayerState.Move);
            
        }
    }
  
    private void CreateEnemies(List<EnemyData> targetEnemyDatas)
    {
        Vector2Int spawnPos = Vector2Int.zero;

        List<Vector2Int> _spawnedCells = new();
        
        for (int index = 0; index < _stage.StageData.MaxNormalEnemyCount; index++)
        {
            EnemyData randEnemyData = targetEnemyDatas[Random.Range(0, targetEnemyDatas.Count)];

            Enemy targetEnemy = Instantiate(randEnemyData.Prefab, _stage.transform);
            
            targetEnemy.Init(randEnemyData, this);
            
            _enemyList.Add(targetEnemy);

            if (targetEnemy.EnemyData.IsBoss)
            {
                targetEnemy.transform.position = transform.position;

                return;
            }
            else
            {
                while (true)
                {
                    var roomSize = _stage.StageData.RoomSize;
                
                    spawnPos = new Vector2Int(
                        Random.Range(roomSize.x/2 * -1 + 2, roomSize.x/2 - 2), 
                        Random.Range(roomSize.y/2 * -1 + 2, roomSize.y/2 - 2));
                    
                    if (!_spawnedCells.Contains(spawnPos))
                    {
                        _spawnedCells.Add(spawnPos);
                
                        targetEnemy.transform.position = transform.position + new Vector3(spawnPos.x, 0, spawnPos.y);
                    
                        break;
                    }
                }
            }
        }
    }

   
}