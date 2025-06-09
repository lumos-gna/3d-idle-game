using UnityEngine;

public class Enemy : CharacterController
{
    public EnemyData EnemyData { get; private set; }

    private PlayerController _targetPlayer;


   
    public void Init(EnemyData enemyData, Room spawnedRoom)
    {
        EnemyData = enemyData;

        CurrentRoom = spawnedRoom;
        
        StatHandler.Init(enemyData.Stats);
    }


    private void Update()
    {
        if (_targetPlayer == null) return;
        
        Move(_targetPlayer.transform.position, out bool isArrived);
    }

    public void Tracking(PlayerController player)
    {
        _targetPlayer = player;
    }

    protected override void Die()
    {
        GameManager.Instance.AddGold(EnemyData.DropGold);

        CurrentRoom.OnEnemyDeath(this);
        
        Destroy(gameObject);
    }
}
