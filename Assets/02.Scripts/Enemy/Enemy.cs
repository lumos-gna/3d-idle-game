using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemyData EnemyData { get; private set; }


    [SerializeField] private float attackRange;
    [SerializeField] private float maxAttackDelay;


    private Room _spawnedRoom;
    private NavMeshAgent _agent;
    private PlayerController _player;
    
    
    private float _currentAttackDelay;
    

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.enabled = false;

        _agent.stoppingDistance = attackRange;
    }

    public void Init(EnemyData enemyData, Room spawnedRoom)
    {
        _spawnedRoom = spawnedRoom;
        
        EnemyData = enemyData;
    }


    private void Update()
    {
        if (!_agent.enabled && _player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer > _agent.stoppingDistance + 0.1f)
        {
            _agent.SetDestination(_player.transform.position);

            _currentAttackDelay = 0;
        }
        else if (IsArrived())
        {
            _currentAttackDelay += Time.deltaTime;

            if (_currentAttackDelay >= maxAttackDelay)
            {
                _currentAttackDelay = 0;
            }
        }
    }

    public void Tracking(PlayerController player)
    {
        _agent.enabled = true;
        _player = player;
    }

    public void Die()
    {
        _spawnedRoom.OnEnemyDeath(this);
        
        Destroy(gameObject);
    }
    
    private bool IsArrived()
    {
        return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
    }
}
