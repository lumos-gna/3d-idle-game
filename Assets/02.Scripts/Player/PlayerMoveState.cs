
public class PlayerMoveState : IPlayerState
{
    public void OnEnter(PlayerController player)
    {
        player.Agent.isStopped = false;
    }

    public void OnUpdate(PlayerController player)
    {
        player.Agent.SetDestination(player.CurrentRoom.transform.position);
    }
}