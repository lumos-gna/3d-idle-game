using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public abstract class CharacterController : MonoBehaviour
{
    public NavMeshAgent NavAgent { get; private set; }
    public StatHandler StatHandler { get; private set; }
    public Room CurrentRoom { get; set; }
    
    protected Dictionary<CharacterStateType, CharacterState> _characterStates;

    protected CharacterState _currentState;

    protected virtual void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        StatHandler = GetComponent<StatHandler>();
        
        _characterStates = new ()
        {
            { CharacterStateType.Move , new CharacterMoveState(this)},
            { CharacterStateType.Combat , new CharacterCombatState(this)}
        };
    }

    protected void Update()
    {
        _currentState?.OnUpdate();
    }

    public abstract CharacterController GetTargetController();

    protected abstract void Die();

    
    public void TakeDamage(float damage)
    {
        if (StatHandler.TryGetStat(StatType.CurHealth, out Stat currentHealth))
        {
            currentHealth.ModifyStatValue(damage * -1);
            
            if (currentHealth.value <= 0)
            {
                Die();
            }
        }
    }
    
    public void Move(Vector3 targetPos, out bool isArrived)
    {
        if (StatHandler.TryGetStat(StatType.AttackRange, out Stat attackRange))
        {
            NavAgent.SetDestination(targetPos);

            isArrived = NavAgent.remainingDistance <= attackRange.value;
        
            NavAgent.isStopped = isArrived;

            return;
        }
        
        isArrived = false;
    }
    
    public void ChangeStage(CharacterStateType stateType)
    {
        if (_characterStates.ContainsKey(stateType))
        {
            _currentState = _characterStates[stateType];
            
            _currentState.OnEnter();
        }
    }
}
