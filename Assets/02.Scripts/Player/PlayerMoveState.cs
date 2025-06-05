
public class PlayerMoveState : IPlayerState
{
    public void OnUpdate(PlayerController player)
    {
        Enemy targetEnemy = player.GetClosestEnemy(player.CurrentStage.CurrentRoom);
                
        player.UpdateMoveTarget(targetEnemy.transform);

        if (player.IsArrived())
        {
            targetEnemy.Die();
        }
    }
}