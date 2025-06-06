
public class PlayerMoveState : IPlayerState
{
    public void OnUpdate(PlayerController player)
    {
        player.UpdateMove(player.CurrentRoom.transform);
    }
}