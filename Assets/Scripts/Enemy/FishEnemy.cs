using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DistantLands;
using IndieMarc.EnemyVision;
using SubNemesis.Enemy;
using SubNemesis.GamePlay;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

namespace SubNemesis.Enemy
{
    [RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(NavMeshAgent))]
    public class FishEnemy : Health
    {
        [SerializeField] private GameObject waypointGameObject;
        [SerializeField] private GameObject enemyDeathVFX;
        [SerializeField] private GameObject enemyHitVFX;

        private Vector3 _playerLocation;
        private IndieMarc.EnemyVision.Enemy _enemy;

        private GameObject[] _enemyPatrolPath;

        private void Awake()
        {
            _enemy = GetComponent<IndieMarc.EnemyVision.Enemy>();

            TryGetComponent(out UbhShotCtrl shotCtrl);
            if (shotCtrl == null)
                gameObject.AddComponent<UbhShotCtrl>();

            var randomWaypoints = gameObject.AddComponent<CreateEnemyWaypoints>();
            randomWaypoints.CreateEnemyRandomWaypoints(waypointGameObject, _enemy);
            
            GetComponent<UbhShotCtrl>().StartShotRoutine();
        }

        private void Start()
        {
            GetComponent<Rigidbody>().useGravity = false;

            onTakeDamageAction += TakeDamage;
        }

        private void Update()
        {
            GetComponent<UbhShotCtrl>().StartShotRoutine();
        }

        public override void TakeDamage(float damageAmount)
        {
            Vector3 position = gameObject.transform.position;
            var identity = quaternion.identity;

            if (_health <= 0)
            {
                Die(position, identity);
            }
            else
            {
                TakeDamage(damageAmount, position, identity);
                if (_health <= 0)
                {
                    Die(position, identity);
                }
            }
        }

        private void TakeDamage(float damageAmount, Vector3 _position, quaternion _identity)
        {
            _health -= damageAmount;
            var hitVFX = Instantiate(enemyHitVFX, _position, _identity);
            Destroy(hitVFX, 2f);
        }

        private void Die(Vector3 position, quaternion rotation)
        {
            var deathVFX = Instantiate(enemyDeathVFX, position, rotation);
            Destroy(deathVFX, 3f);
            onTakeDamageAction -= TakeDamage;
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            GetComponent<UbhShotCtrl>().StopShotRoutineAndPlayingShot();
        }
    }
}