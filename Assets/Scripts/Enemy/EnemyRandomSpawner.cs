﻿using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyRandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyGo;
    [SerializeField] private int numberOfEnemiesToSpawn = 9;
    [SerializeField] private Transform enemiesParentTransform;

    private readonly List<EnemySpawn> _enemySpawns = new List<EnemySpawn>();
    private readonly Random _randomSpawner = new Random();
    private readonly Random _randomEnemyType = new Random();

    private void Awake()
    {
        var spawners = GetComponentsInChildren<EnemySpawn>();
        foreach (var enemySpawn in spawners)
        {
            _enemySpawns.Add(enemySpawn);
        }

        for (var ctr = 0; ctr < numberOfEnemiesToSpawn; ++ctr)
        {
            var spawnIndex = _randomSpawner.Next(_enemySpawns.Count);
            var enemyIndex = _randomEnemyType.Next(enemyGo.Length);

            var parentTransform = _enemySpawns[spawnIndex].transform;
            var enemy = Instantiate(enemyGo[enemyIndex], parentTransform.position, parentTransform.rotation);

            enemy.transform.SetParent(enemiesParentTransform);
            //_enemySpawns.Remove(_enemySpawns[spawnIndex]);
        }

        // foreach (var enemySpawn in _enemySpawns)
        // {
        //     enemySpawn.gameObject.SetActive(false);
        // }
    }

    public void SpawnEnemies()
    {
        var spawners = GetComponentsInChildren<EnemySpawn>();
        foreach (var runeSpawn in spawners)
        {
            _enemySpawns.Add(runeSpawn);
        }

        for (var ctr = 0; ctr < numberOfEnemiesToSpawn; ++ctr)
        {
            var spawnIndex = _randomSpawner.Next(_enemySpawns.Count);
            var runeIndex = _randomEnemyType.Next(enemyGo.Length);

            var parentTransform = _enemySpawns[spawnIndex].transform;
            var rune = Instantiate(enemyGo[runeIndex], parentTransform.position, parentTransform.rotation);

            rune.transform.SetParent(parentTransform);
            _enemySpawns.Remove(_enemySpawns[spawnIndex]);
        }

        foreach (var vRuneSpawn in _enemySpawns)
        {
            vRuneSpawn.gameObject.SetActive(false);
        }
    }
}