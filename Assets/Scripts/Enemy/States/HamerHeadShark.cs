public class HammerHeadShark : EnemyTypeBehaviour
{
    public override void UpdateState(FishEnemy enemy, float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        enemy.HamerHeadSharkBehaviour(playerLocationX, playerLocationY, playerLocationZ);
    }
}