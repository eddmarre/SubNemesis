using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private float[] waveTimeValue;
    [SerializeField] private int[] waveSpawnNumbers;
    [SerializeField] private TextMeshProUGUI waveCountText;
    private int _waveCounter;

    private EnemyRandomSpawner _enemySpawn;

    private void Start()
    {
        _enemySpawn = FindObjectOfType<EnemyRandomSpawner>();
    }

    private void OnValidate()
    {
        if (waveSpawnNumbers.Length != waveTimeValue.Length)
        {
            Debug.LogWarning("Both Lenghts must match", this);
        }

        if (waveSpawnNumbers.Length == 0)
            Debug.LogWarning("No waves spawn numbers were created", this);

        if (waveTimeValue.Length == 0)
            Debug.LogWarning("No time values for waves", this);
    }

    private void Update()
    {
        waveCountText.text = _waveCounter.ToString();
        if (_waveCounter > waveSpawnNumbers.Length - 1 || _waveCounter > waveTimeValue.Length - 1)
        {
            Debug.Log("No more spawns", this);
        }
        else if (waveTimeValue[_waveCounter] > 0)
        {
            waveTimeValue[_waveCounter] -= Time.deltaTime;
        }
        else if (waveTimeValue[_waveCounter] <= 0)
        {
            _enemySpawn.SpawnEnemies(waveSpawnNumbers[_waveCounter]);
            _waveCounter++;
        }
        else
        {
            waveTimeValue[_waveCounter] = 0;
        }
    }
}