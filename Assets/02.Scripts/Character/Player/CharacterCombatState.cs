using UnityEngine;

public class CharacterCombatState : CharacterState
{
    public override CharacterStateType StateType => CharacterStateType.Combat;

    private float _attackTimer;

    public CharacterCombatState(CharacterController controller) : base(controller) {}


    public override void OnEnter()
    {
        _controller.NavAgent.isStopped = false;
    }
    
    public override void OnUpdate()
    {
        if (_controller.CurrentRoom != null)
        {
            if (_controller.CurrentRoom.Enemies.Count > 0)
            {
                CharacterController target = _controller.GetTargetController();

                if (target == null) return;
            
                _controller.Move(target.transform.position, out bool isArrived);

                if (isArrived)
                {
                    _attackTimer += Time.deltaTime;

                    if (_controller.StatHandler.TryGetStat(StatType.AttackSpeed, out Stat attackSpeed))
                    {
                        if (_attackTimer >= attackSpeed.value)
                        {
                            if (_controller.StatHandler.TryGetStat(StatType.Damage, out Stat damage))
                            {
                                target.TakeDamage(damage.value);
                                
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