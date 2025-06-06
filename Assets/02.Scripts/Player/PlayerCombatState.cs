
public class PlayerCombatState : IPlayerState
{
    public void OnUpdate(PlayerController player)
    {
        if (player.CurrentRoom.Enemies.Count > 0)
        {
            var target = player.GetClosestRoomEnemy();

            player.UpdateMove(target.transform);

            if (player.IsArrived())
            {
                target.Die();
            }
        }
    }
}