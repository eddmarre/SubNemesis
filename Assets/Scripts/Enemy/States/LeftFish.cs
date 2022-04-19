
    public class LeftFish:EnemyTypeBehaviour
    {
        public override void UpdateState(FishEnemy enemy, float playerLocationX, float playerLocationY, float playerLocationZ)
        {
            enemy.LeftFishBehaviour(playerLocationX, playerLocationY, playerLocationZ);
        }
    }
