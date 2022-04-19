
    public class LeftFish:EnemyTypeBehaviour
    {
        public override void UpdateState(Enemy enemy, float playerLocationX, float playerLocationY, float playerLocationZ)
        {
            enemy.LeftFishBehaviour(playerLocationX, playerLocationY, playerLocationZ);
        }
    }
