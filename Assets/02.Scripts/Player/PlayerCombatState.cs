
using UnityEngine;

public class PlayerCombatState : IPlayerState
{
    private float _attackTimer;
    
    public void OnEnter(PlayerController player)
    {
        player.Agent.isStopped = false;
    }
    
    public void OnUpdate(PlayerController player)
    {
        if (player.CurrentRoom.Enemies.Count > 0)
        {
            var enemy = player.GetClosestRoomEnemy();
            
            player.Agent.SetDestination(enemy.transform.position);

            if (player.IsArrived())
            {
                player.Agent.isStopped = true;
                
                _attackTimer += Time.deltaTime;
                
                if (_attackTimer >= player.StatHandler.GetStat(StatType.AttackSpeed))
                {
                    enemy.TakeDamage(player.StatHandler.GetStat(StatType.Damage));

                    _attackTimer = 0;
                }
                
            }
            else
            {
                player.Agent.isStopped = false;
                
                _attackTimer = 0;
            }
        }
    }
}