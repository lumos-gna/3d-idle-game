using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public abstract class CharacterController : MonoBehaviour
{
    public NavMeshAgent NavAgent => navAgent;
    public StatHandler StatHandler => statHandler;
    
    public Room CurrentRoom { get; set; }

    
    [SerializeField] private StatHandler statHandler;
    [SerializeField] private NavMeshAgent navAgent;
    
    
    protected Dictionary<CharacterStateType, CharacterState> _characterStates;

    protected CharacterState _currentState;

    protected virtual void Awake()
    {
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

    
    public virtual void TakeDamage(float damage)
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
