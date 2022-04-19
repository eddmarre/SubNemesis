public class HammerHeadShark : EnemyTypeBehaviour
{
    public override void UpdateState(Enemy enemy, float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        enemy.HamerHeadSharkBehaviour(playerLocationX, playerLocationY, playerLocationZ);
    }
}