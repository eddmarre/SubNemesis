public class GreatWhiteShark : EnemyTypeBehaviour
{
    public override void UpdateState(FishEnemy enemy, float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        enemy.GreatWhiteSharkBehaviour(playerLocationX, playerLocationY, playerLocationZ);
    }
}