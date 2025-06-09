using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData { get; private set; }

    private Room _spawnedRoom;
    private NavMeshAgent _agent;
    private PlayerController _player;
    private StatHandler _statHandler;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _statHandler = GetComponent<StatHandler>();

    }

    public void Init(EnemyData enemyData, Room spawnedRoom)
    {
        EnemyData = enemyData;

        _spawnedRoom = spawnedRoom;
        
        _statHandler.Init(EnemyData.StatDatas);
    }


    private void Update()
    {
        if (_player == null) return;
        
        _agent.SetDestination(_player.transform.position);
        
        _agent.isStopped = IsArrived();
    }

    public void Tracking(PlayerController player)
    {
        _player = player;
    }

    public void TakeDamage(float damage)
    {
        _statHandler.ModifyStat(StatType.Health, damage * -1);
        
        if (_statHandler.GetStat(StatType.Health) <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.Instance.AddGold(EnemyData.DropGold);

        _spawnedRoom.OnEnemyDeath(this);
        
        Destroy(gameObject);
    }
    
    private bool IsArrived()
    {
        return _agent.remainingDistance <= _statHandler.GetStat(StatType.AttackRange);
    }
   
}
