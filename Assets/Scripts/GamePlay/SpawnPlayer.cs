using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubNemesis.GamePlay
{
    public class SpawnPlayer : MonoBehaviour
    {
        [SerializeField] private GameObject player;

        private void Start()
        {
            Instantiate(player, transform.position, Quaternion.identity);
        }
    }
}