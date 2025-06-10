using System;
using UnityEngine;

public class Enemy : CharacterController
{
    public EnemyData EnemyData { get; private set; }


    private PlayerController _targetPlayer;
    

    protected override void Awake()
    {
        base.Awake();

        NavAgent.enabled = false;
    }

    public void Init(EnemyData enemyData, Room spawnedRoom)
    {
        EnemyData = enemyData;

        CurrentRoom = spawnedRoom;
        
        StatHandler.Init(enemyData.Stats);
        
        NavAgent.enabled = true;
    }

    public void SetTargetPlayer(PlayerController player)
    {
        _targetPlayer = player;
        
        ChangeStage(CharacterStateType.Combat);
    }

    public override CharacterController GetTargetController()
    {
        return _targetPlayer != null?  _targetPlayer : null;
    }

    protected override void Die()
    {
        GameManager.Instance.AddGold(EnemyData.DropGold);

        CurrentRoom.OnEnemyDeath(this);
        
        Destroy(gameObject);
    }
}
