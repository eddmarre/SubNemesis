using System;
using System.Collections.Generic;
using IndieMarc.EnemyVision;
using UnityEngine;
using Random = System.Random;

namespace SubNemesis.Enemy
{
    public class CreateEnemyWaypoints : MonoBehaviour
    {
        [SerializeField] private int minRandomWaypointSpawnDistance = -15;
        [SerializeField] private int maxRandomWaypointSpawnDistance = 15;
        [SerializeField] private int createNumberOfWaypoints = 4;
        private GameObject waypointParentGameObject;

        private GameObject[] enemyPatrolPath;


        public void CreateEnemyRandomWaypoints(GameObject FishEnemywaypointGameObject, IndieMarc.EnemyVision.Enemy myEnemy)
        {
            waypointParentGameObject = FindObjectOfType<EnemyDestination>().gameObject;
            List<GameObject> createdWayPoints = new List<GameObject>();
            Random randomNumber = new Random();
            for (int i = 0; i < createNumberOfWaypoints; i++)
            {
                var xRandomPosition = randomNumber.Next(minRandomWaypointSpawnDistance, maxRandomWaypointSpawnDistance);
                var zRandomPosition = randomNumber.Next(minRandomWaypointSpawnDistance, maxRandomWaypointSpawnDistance);

                var unityxRandomPosition =
                    UnityEngine.Random.Range(minRandomWaypointSpawnDistance, maxRandomWaypointSpawnDistance);
                var unityZRandomPosition =
                    UnityEngine.Random.Range(minRandomWaypointSpawnDistance, maxRandomWaypointSpawnDistance);
                var randomLocation =
                    new Vector3(unityxRandomPosition, transform.position.y,
                        unityZRandomPosition);
                var newWaypoint = Instantiate(FishEnemywaypointGameObject, randomLocation, Quaternion.identity,
                    waypointParentGameObject.transform);
                createdWayPoints.Add(newWaypoint);
                randomNumber.Next(minRandomWaypointSpawnDistance, maxRandomWaypointSpawnDistance);
            }

            enemyPatrolPath = new GameObject[createdWayPoints.Count];

            for (int i = 0; i < createdWayPoints.Count; i++)
            {
                enemyPatrolPath[i] = createdWayPoints[i];
            }

            myEnemy.patrol_path = enemyPatrolPath;
        }

        private void OnDestroy()
        {
            foreach (var spawn in enemyPatrolPath)
            {
                Destroy(spawn);
            }
        }
    }
}