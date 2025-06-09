
using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    private PlayerController _player;
    
    public PlayerMoveState(PlayerController player)
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
            _player.Move(_player.CurrentRoom.transform.position, out bool isArrived);
        }
    }
}