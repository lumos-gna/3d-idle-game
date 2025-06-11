using UnityEngine;

public class CharacterMoveState : CharacterState
{
    public override CharacterStateType StateType => CharacterStateType.Move;
    
    
    public CharacterMoveState(CharacterController controller) : base(controller){ }

    public override void OnEnter()
    {
        _controller.NavAgent.isStopped = false;
    }

    public override void OnUpdate()
    {
        if (_controller.CurrentRoom != null)
        {
            _controller.Move(_controller.CurrentRoom.transform.position, out bool isArrived);
        }
    }
}