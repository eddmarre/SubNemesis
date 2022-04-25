using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace SubNemesis.Enemy
{
    public class EnemyRandomSpawner : MonoBehaviour
    {
        [SerializeField] private Transform enemiesParentTransform;

        private readonly List<EnemySpawn> _enemySpawns = new List<EnemySpawn>();

        private readonly Random _randomSpawner = new Random();
        private readonly Random _randomEnemyType = new Random();

        public void SpawnEnemies(int _numberOfEnemiesToSpawn, GameObject[] _enemies)
        {
            var spawners = GetComponentsInChildren<EnemySpawn>();
            foreach (var enemySpawn in spawners)
            {
                _enemySpawns.Add(enemySpawn);
            }

            for (var ctr = 0; ctr < _numberOfEnemiesToSpawn; ++ctr)
            {
                var spawnIndex = _randomSpawner.Next(_enemySpawns.Count);
                var enemyIndex = _randomEnemyType.Next(_enemies.Length);

                var parentTransform = _enemySpawns[spawnIndex].transform;
                var enemy = Instantiate(_enemies[enemyIndex], parentTransform.position, parentTransform.rotation);

                enemy.transform.SetParent(enemiesParentTransform);
                _enemySpawns.Remove(_enemySpawns[spawnIndex]);
            }
        }
    }
}