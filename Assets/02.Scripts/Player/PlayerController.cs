using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Stage CurrentStage { get; private set; }

    private NavMeshAgent _agent;


    private Dictionary<PlayerState, IPlayerState> _playerStates;

    private IPlayerState _currentState;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _playerStates = new ()
        {
            { PlayerState.Move , new PlayerMoveState()}
        };
    }
    

    private void Update()
    {
        if (CurrentStage != null)
        {
            var currentEnemies = CurrentStage.CurrentRoom.Enemies;
            
            if (currentEnemies.Count > 0)
            {
                _currentState.OnUpdate(this);
            }   
        }
    }
    
    
    public void StartStage(Stage stage)
    {
        CurrentStage = stage;

        transform.position = CurrentStage.CurrentRoom.transform.position;

        ChangeStage(PlayerState.Move);
    }


    public void ChangeStage(PlayerState state)
    {
        if (_playerStates.ContainsKey(state))
        {
            _currentState = _playerStates[state];
        }
    }
    

    public void UpdateMoveTarget(Transform target)
    {
        _agent.SetDestination(target.position);
    }
    
    public bool IsArrived()
    {
        return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
    }




    public Enemy GetClosestEnemy(Room currentRoom)
    {
        Enemy closestEnemy = null;

        float closestDist = float.MaxValue;
        
        for (int i = 0; i < currentRoom.Enemies.Count; i++)
        {
            float currentDist = (currentRoom.Enemies[i].transform.position - transform.position).sqrMagnitude;
            
            if (currentDist < closestDist)
            {
                closestDist = currentDist;

                closestEnemy = currentRoom.Enemies[i];
            }
        }
        
        return closestEnemy;
    }
}
