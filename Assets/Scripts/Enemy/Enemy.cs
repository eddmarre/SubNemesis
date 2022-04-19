using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DistantLands;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

[RequireComponent(typeof(BoxCollider), typeof(Rigidbody), typeof(NavMeshAgent))]
public class Enemy : Health
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

    [SerializeField] private EnemyType enemyType;

    private void OnValidate()
    {
        gameObject.name = enemyType.ToString();
    }

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        TryGetComponent(out UbhShotCtrl shotCtrl);
        if (shotCtrl == null)
        {
            gameObject.AddComponent<UbhShotCtrl>();
        }

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

    private void Start()
    {
        _navMeshAgent.speed = 5f;
        GetComponent<Rigidbody>().useGravity = false;
        onTakeDamageAction += TakeDamage;
    }


    private void Update()
    {
        Debug.Log($"{gameObject.name} {_health}");
        var playerLocation = FindObjectOfType<SubmarineController>().transform.position;

        currentState.UpdateState(this, playerLocation.x, playerLocation.y, playerLocation.z);

        GetComponent<UbhShotCtrl>().StartShotRoutine();
        transform.LookAt(playerLocation);
    }


    public override void TakeDamage(float damageAmount)
    {
        if (_health <= 0)
        {
            Debug.Log($"{gameObject.name} is dead  ", this);
            onTakeDamageAction -= TakeDamage;
            Destroy(gameObject);
        }
        else
        {
            _health -= damageAmount;
            if (_health <= 0)
            {
                onTakeDamageAction -= TakeDamage;
                Destroy(gameObject);
            }
        }
    }

    public void HamerHeadSharkBehaviour(float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        var _playerLoaction = new Vector3(playerLocationX, playerLocationY, playerLocationZ);
        Random random = new Random();
        var xOffset = random.Next(-20,20);
        var zOffset = random.Next(-20,20);
        _navMeshAgent.SetDestination(_playerLoaction + new Vector3(xOffset, 0, 10 + zOffset));
    }

    public void GreatWhiteSharkBehaviour(float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        Random random = new Random();
        var xOffset = random.Next(-20,20);
        var zOffset = random.Next(-20,20);
        var _playerLoaction = new Vector3(playerLocationX, playerLocationY, playerLocationZ);
        _navMeshAgent.SetDestination(_playerLoaction + new Vector3(xOffset, 0, -10 + zOffset));
    }

    public void RightFishBehaviour(float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        Random random = new Random();
        var xOffset = random.Next(-20,20);
        var zOffset = random.Next(-20,20);
        var _playerLoaction = new Vector3(playerLocationX, playerLocationY, playerLocationZ);
        _navMeshAgent.SetDestination(_playerLoaction + new Vector3(10 + xOffset, 0, zOffset));
    }

    public void LeftFishBehaviour(float playerLocationX, float playerLocationY, float playerLocationZ)
    {
        Random random = new Random();
        var xOffset = random.Next(-5, 5);
        var zOffset = random.Next(-5, 5);
        var playerLoaction = new Vector3(playerLocationX, playerLocationY, playerLocationZ);
        _navMeshAgent.SetDestination(playerLoaction + new Vector3(-10 + xOffset, 0, zOffset));
    }

    public void SwitchState(EnemyTypeBehaviour state)
    {
        currentState = state;
    }
}