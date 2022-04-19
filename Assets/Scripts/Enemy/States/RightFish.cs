public class RightFish : EnemyTypeBehaviour
{
    public override void UpdateState(Enemy enemy, float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        enemy.RightFishBehaviour(playerLocationX, playerLocationY, playerLocationZ);
    }
}