using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField] private CharacterData playerData;

    private Dictionary<PlayerState, IPlayerState> _playerStates;

    private IPlayerState _currentState;

    
    protected override void Awake()
    {
        base.Awake();
        
        _playerStates = new ()
        {
            { PlayerState.Move , new PlayerMoveState(this)},
            { PlayerState.Combat , new PlayerCombatState(this)}
        };
        
        StatHandler.Init(playerData.Stats);
    }

    private void Update()
    {
        _currentState?.OnUpdate();
    }
    
    protected override void Die()
    {
    }
  
    
    public void Init()
    {
        NavAgent.enabled = false;
        
        transform.position = Vector3.zero;
        
        NavAgent.enabled = true;
    }


    public void ChangeStage(PlayerState state)
    {
        if (_playerStates.ContainsKey(state))
        {
            _currentState = _playerStates[state];
            
            _currentState.OnEnter();
        }
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
