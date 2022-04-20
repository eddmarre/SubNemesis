using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DistantLands;
using IndieMarc.EnemyVision;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(NavMeshAgent))]
public class FishEnemy : Health
{
    enum EnemyType
    {
        GreatWhiteShark,
        HamerHeadShark,
        RightFish,
        LeftFish
    }

    private EnemyTypeBehaviour currentState;
    private EnemyTypeBehaviour hammerHead = new HammerHeadShark();
    private EnemyTypeBehaviour greatWhite = new GreatWhiteShark();
    private EnemyTypeBehaviour rightFish = new RightFish();
    private EnemyTypeBehaviour leftFish = new LeftFish();


    private NavMeshAgent _navMeshAgent;
    private Vector3 playerLocation;

    private Enemy _enemy;

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private GameObject waypointGameObject;

    [SerializeField] private int minRandomWaypointSpawnDistance = -25;
    [SerializeField] private int maxRandomWaypointSpawnDistance = 25;
    [SerializeField] private int createNumberOfWaypoints = 4;
    private GameObject waypointParentGameObject;

    [SerializeField] private GameObject enemyDeathVFX;
    [SerializeField] private GameObject enemyHitVFX;


    private GameObject[] enemyPatrolPath;


    private void OnValidate()
    {
        gameObject.name = enemyType.ToString();
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _enemy = GetComponent<Enemy>();

        TryGetComponent(out UbhShotCtrl shotCtrl);
        if (shotCtrl == null)
            gameObject.AddComponent<UbhShotCtrl>();

        AssignEnemyType();

        CreateEnemyRandomWaypoints();
    }

    private void AssignEnemyType()
    {
        if (enemyType == EnemyType.HamerHeadShark)
        {
            currentState = hammerHead;
        }

        if (enemyType == EnemyType.GreatWhiteShark)
        {
            currentState = greatWhite;
        }

        if (enemyType == EnemyType.RightFish)
        {
            currentState = rightFish;
        }

        if (enemyType == EnemyType.LeftFish)
        {
            currentState = leftFish;
        }
    }

    private void CreateEnemyRandomWaypoints()
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
            var newWaypoint = Instantiate(waypointGameObject, randomLocation, Quaternion.identity,
                waypointParentGameObject.transform);
            createdWayPoints.Add(newWaypoint);
            randomNumber.Next(minRandomWaypointSpawnDistance, maxRandomWaypointSpawnDistance);
        }

        enemyPatrolPath = new GameObject[createdWayPoints.Count];

        for (int i = 0; i < createdWayPoints.Count; i++)
        {
            enemyPatrolPath[i] = createdWayPoints[i];
        }

        _enemy.patrol_path = enemyPatrolPath;
    }


    private void Start()
    {
        _navMeshAgent.speed = 5f;
        GetComponent<Rigidbody>().useGravity = false;
        onTakeDamageAction += TakeDamage;
    }


    private void Update()
    {
//        Debug.Log($"{gameObject.name} {_health}");
        var playerLocation = FindObjectOfType<SubmarineController>().transform.position;

        currentState.UpdateState(this, playerLocation.x, playerLocation.y, playerLocation.z);

        GetComponent<UbhShotCtrl>().StartShotRoutine();
        // transform.LookAt(playerLocation);
    }


    public override void TakeDamage(float damageAmount)
    {
        if (_health <= 0)
        {
            var deathVFX = Instantiate(enemyDeathVFX, transform.position, quaternion.identity);
            Destroy(deathVFX, 3f);
            onTakeDamageAction -= TakeDamage;
            Destroy(gameObject);
        }
        else
        {
            _health -= damageAmount;
            var hitVFX = Instantiate(enemyHitVFX, transform.position, quaternion.identity);
            Destroy(hitVFX, 2f);
            if (_health <= 0)
            {
                onTakeDamageAction -= TakeDamage;
                var deathVFX = Instantiate(enemyDeathVFX, transform.position, quaternion.identity);
                Destroy(deathVFX, 3f);
                Destroy(gameObject);
            }
        }
    }


    public void HamerHeadSharkBehaviour(float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        // var _playerLoaction = new Vector3(playerLocationX, playerLocationY, playerLocationZ);
        // Random random = new Random();
        // var xOffset = random.Next(-20,20);
        // var zOffset = random.Next(-20,20);
        // _navMeshAgent.SetDestination(_playerLoaction + new Vector3(xOffset, 0, 10 + zOffset));
    }

    public void GreatWhiteSharkBehaviour(float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        // Random random = new Random();
        // var xOffset = random.Next(-20,20);
        // var zOffset = random.Next(-20,20);
        // var _playerLoaction = new Vector3(playerLocationX, playerLocationY, playerLocationZ);
        // _navMeshAgent.SetDestination(_playerLoaction + new Vector3(xOffset, 0, -10 + zOffset));
    }

    public void RightFishBehaviour(float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        // Random random = new Random();
        // var xOffset = random.Next(-20,20);
        // var zOffset = random.Next(-20,20);
        // var _playerLoaction = new Vector3(playerLocationX, playerLocationY, playerLocationZ);
        // _navMeshAgent.SetDestination(_playerLoaction + new Vector3(10 + xOffset, 0, zOffset));
    }

    public void LeftFishBehaviour(float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        // Random random = new Random();
        // var xOffset = random.Next(-5, 5);
        // var zOffset = random.Next(-5, 5);
        // var playerLoaction = new Vector3(playerLocationX, playerLocationY, playerLocationZ);
        // _navMeshAgent.SetDestination(playerLoaction + new Vector3(-10 + xOffset, 0, zOffset));
    }

    public void SwitchState(EnemyTypeBehaviour state)
    {
        currentState = state;
    }

    private void OnDestroy()
    {
        foreach (var spawn in enemyPatrolPath)
        {
            Destroy(spawn);
        }
        GetComponent<UbhShotCtrl>().StopShotRoutineAndPlayingShot();
    }
}