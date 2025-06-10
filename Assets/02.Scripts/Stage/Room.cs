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
                    _enemyList[i].SetTargetPlayer(player);
                }
                
                _player.ChangeStage(CharacterStateType.Combat);
            }
            else
            {
                ClearRoom();
            }
        }
    }

    public void Init(Stage stage)
    {
        _stage = stage;

        triggerCollider.enabled = true;
    }
    
    
    public void OnEnemyDeath(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        
        if (_enemyList.Count == 0)
        {
            ClearRoom();
        }
    }
    
    public void CreateEnemies(bool isBoss, StageEnemyInfo enemyInfo)
    {
        Vector2Int spawnPos = Vector2Int.zero;

        List<Vector2Int> _spawnedCells = new();
        
        for (int index = 0; index < enemyInfo.maxSpawnCount; index++)
        {
            EnemyData randEnemyData = enemyInfo.enemyDatas[Random.Range(0, enemyInfo.enemyDatas.Length)];

            Enemy targetEnemy = Instantiate(randEnemyData.Prefab, _stage.transform);
            
            if (isBoss)
            {
                targetEnemy.transform.position = transform.position;
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
            
            targetEnemy.Init(randEnemyData, this);
            
            _enemyList.Add(targetEnemy);
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
            _player.ChangeStage(CharacterStateType.Move);
            
        }
    }
  
 
   
}