using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Room CurrentRoom { get; private set; }

    private NavMeshAgent _agent;

    private Dictionary<PlayerState, IPlayerState> _playerStates;

    private IPlayerState _currentState;

    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();

        _playerStates = new ()
        {
            { PlayerState.Move , new PlayerMoveState()},
            { PlayerState.Combat , new PlayerCombatState()}
        };
    }
    

    private void Update()
    {
        if (_agent.enabled && CurrentRoom != null)
        {
            _currentState.OnUpdate(this);
        }
    }
    
    public void Init()
    {
        _agent.enabled = false;
        
        transform.position = Vector3.zero;
        
        _agent.enabled = true;
    }


    public void ChangeStage(PlayerState state)
    {
        if (_playerStates.ContainsKey(state))
        {
            _currentState = _playerStates[state];
        }
    }

    public void UpdateMove(Transform target)
    {
        _agent.SetDestination(target.position);
    }

    public void MoveToNextRoom(Room targetRoom)
    {
        ChangeStage(PlayerState.Move);

        CurrentRoom = targetRoom;
    }

    public void EnterCombat()
    {
        ChangeStage(PlayerState.Combat);
    }
    
    public bool IsArrived()
    {
        return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
    }


    public Enemy GetClosestRoomEnemy()
    {
        Enemy closestEnemy = null;

        float closestDist = float.MaxValue;
        
        for (int i = 0; i < CurrentRoom.Enemies.Count; i++)
        {
            float currentDist = (CurrentRoom.Enemies[i].transform.position - transform.position).sqrMagnitude;
            
            if (currentDist < closestDist)
            {
                closestDist = currentDist;

                closestEnemy = CurrentRoom.Enemies[i];
            }
        }
        
        return closestEnemy;
    }
}
