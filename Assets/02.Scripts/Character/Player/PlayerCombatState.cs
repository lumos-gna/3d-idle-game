using UnityEngine;

public class PlayerCombatState : IPlayerState
{
    private PlayerController _player;
    
    private float _attackTimer;

    public PlayerCombatState(PlayerController player)
    {
        _player = player;
    }
    
    public void OnEnter()
    {
        _player.NavAgent.isStopped = false;
    }
    
    public void OnUpdate()
    {
        if (_player.CurrentRoom != null)
        {
            if (_player.CurrentRoom.Enemies.Count > 0)
            {
                var enemy = _player.GetClosestRoomEnemy();
            
                _player.Move(enemy.transform.position, out bool isArrived);

                if (isArrived)
                {
                    _attackTimer += Time.deltaTime;

                    if (_player.StatHandler.TryGetStat(StatType.AttackSpeed, out Stat attackSpeed))
                    {
                        if (_attackTimer >= attackSpeed.curValue)
                        {
                            if (_player.StatHandler.TryGetStat(StatType.Damage, out Stat damage))
                            {
                                enemy.TakeDamage(damage.curValue);

                                _attackTimer = 0;
                            }
                        }
                    }
                }
                else
                {
                    _attackTimer = 0;
                }
            }
        }
    }
}