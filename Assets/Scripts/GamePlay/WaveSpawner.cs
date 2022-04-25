using System;
using System.Collections;
using System.Collections.Generic;
using SubNemesis.Enemy;
using UnityEngine;
using TMPro;

namespace SubNemesis.GamePlay
{
    public class WaveSpawner : MonoBehaviour
    {
        [SerializeField] private Wave[] waves;
        [SerializeField] private TextMeshProUGUI waveCountText;

        private int _waveCounter = 0;
        private List<float> waveTimes = new List<float>();
        private int enemyCount;

        private EnemyRandomSpawner _enemySpawn;

        private void Start()
        {
            _enemySpawn = FindObjectOfType<EnemyRandomSpawner>();
            foreach (var waveTime in waves)
            {
                waveTimes.Add(waveTime.timeToSpawn);
            }

            enemyCount = 0;
        }


        private void Update()
        {
            waveCountText.text = _waveCounter.ToString();
            enemyCount = FindObjectsOfType<FishEnemy>().Length;
            if (waves.Length == 0)
            {
                Debug.LogError("No waves set");
            }

            if (_waveCounter > waves.Length - 1)
            {
                Debug.Log("No more spawns", this);
            }
            else if (waveTimes[_waveCounter] > 0)
            {
                waveTimes[_waveCounter] -= Time.deltaTime;
            }
            else if (waveTimes[_waveCounter] <= 0 && enemyCount <= 3)
            {
                _enemySpawn.SpawnEnemies(waves[_waveCounter].numberOfEnemiesToSpawn, waves[_waveCounter].enemies);
                _waveCounter++;
            }
            else
            {
                waveTimes[_waveCounter] = 0;
            }
        }
    }
}