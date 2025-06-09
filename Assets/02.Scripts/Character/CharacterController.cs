using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class CharacterController : MonoBehaviour
{
    public NavMeshAgent NavAgent { get; private set; }
    public StatHandler StatHandler { get; private set; }
    public Room CurrentRoom { get; set; }

    protected virtual void Awake()
    {
        NavAgent = GetComponent<NavMeshAgent>();
        StatHandler = GetComponent<StatHandler>();
    }

    protected abstract void Die();

    
    public void TakeDamage(float damage)
    {
        if (StatHandler.TryGetStat(StatType.Health, out Stat health))
        {
            StatHandler.ModifyStatCurValue(StatType.Health, damage * -1);
        
            if (health.curValue <= 0)
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

            isArrived = NavAgent.remainingDistance <= attackRange.curValue;
        
            NavAgent.isStopped = isArrived;

            return;
        }
        
        isArrived = false;
    }
}
