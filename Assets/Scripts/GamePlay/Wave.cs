using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Wave", menuName = "createWave", order = 0)]
public class Wave : ScriptableObject
{
    public float timeToSpawn;
    public int numberOfEnemiesToSpawn;
    public GameObject[] enemies;
}