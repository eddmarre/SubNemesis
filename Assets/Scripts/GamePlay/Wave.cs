using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SubNemesis.GamePlay
{
    [CreateAssetMenu(fileName = "Wave", menuName = "createWave", order = 0)]
    public class Wave : ScriptableObject
    {
        public float timeToSpawn;
        public int numberOfEnemiesToSpawn;
        public GameObject[] enemies;
    }
}