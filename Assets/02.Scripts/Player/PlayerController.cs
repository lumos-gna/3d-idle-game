using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public StatHandler StatHandler { get; private set; }
    public Room CurrentRoom { get; private set; }
    public NavMeshAgent Agent { get; private set; }


    [SerializeField] private PlayerData playerData;
    [SerializeField] private HealthBar helHealthBar;
    
    

    private Dictionary<PlayerState, IPlayerState> _playerStates;

    private IPlayerState _currentState;

    
    private void Awake()
    {
        StatHandler = GetComponent<StatHandler>();
        
        Agent = GetComponent<NavMeshAgent>();

        _playerStates = new ()
        {
            { PlayerState.Move , new PlayerMoveState()},
            { PlayerState.Combat , new PlayerCombatState()}
        };
        
        StatHandler.Init(playerData.StatDatas);
    }
    

    private void Update()
    {
        if (Agent.enabled && CurrentRoom != null)
        {
            _currentState.OnUpdate(this);
        }
    }
    
    public void Init()
    {
        Agent.enabled = false;
        
        transform.position = Vector3.zero;
        
        Agent.enabled = true;
    }


    public void ChangeStage(PlayerState state)
    {
        if (_playerStates.ContainsKey(state))
        {
            _currentState = _playerStates[state];
            
            _currentState.OnEnter(this);
        }
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
        return Agent.remainingDistance <= StatHandler.GetStat(StatType.AttackRange);
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
