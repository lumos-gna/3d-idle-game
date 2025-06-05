using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public event UnityAction OnClearedRoom;

    public int RoomIndex { get; private set; }
    public int MaxEnemyCount { get; private set; }
    
    public List<Enemy> Enemies { get; private set; } = new();

    
    private List<Vector2Int> _spawnedCells = new();
    
    private Vector2Int _roomSize;
    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerController player))
        {
            if (Enemies.Count > 0)
            {
                for (int i = 0; i < Enemies.Count; i++)
                {
                    Enemies[i].Tracking(player);
                }
            }
            else
            {
                OnClearedRoom?.Invoke();
            }
        }
    }
    
    public void Init(int roomIndex, int maxEnemyCount, Vector2Int roomSize, UnityAction onClearedRoom)
    {
        RoomIndex = roomIndex;

        MaxEnemyCount = maxEnemyCount;

        _roomSize = roomSize;

        OnClearedRoom = onClearedRoom;
    }

    public void AddEnemy(Enemy enemy)
    {
        Enemies.Add(enemy);

        SetEnemyPos(enemy);
        
        enemy.OnDeath += OnEnemyDeath;
    }


    void OnEnemyDeath(Enemy enemy)
    {
        Enemies.Remove(enemy);

        if (Enemies.Count == 0)
        {
            OnClearedRoom?.Invoke();
        }
    }

    void SetEnemyPos(Enemy enemy)
    {
        Vector2Int spawnPos = Vector2Int.zero;

        if (enemy.EnemyData.IsBoss)
        {
            enemy.transform.position = transform.position;
        }
        else
        {
            while (true)
            {
                spawnPos = new Vector2Int(
                    Random.Range(_roomSize.x/2 * -1 + 1, _roomSize.x/2 - 1), 
                    Random.Range(_roomSize.y/2 * -1 + 1, _roomSize.y/2 - 1));
            
                if (!_spawnedCells.Contains(spawnPos))
                {
                    _spawnedCells.Add(spawnPos);
                
                    enemy.transform.position = transform.position + new Vector3(spawnPos.x, 1, spawnPos.y);

                    break;
                }
            }
        }
    }
}