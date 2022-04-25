using System;
using UnityEngine;

namespace SubNemesis.GamePlay
{

    public abstract class Health : MonoBehaviour
    {
        [SerializeField] protected float _health = 100f;
        public Action<float> onTakeDamageAction;
        public abstract void TakeDamage(float damageAmount);
    }
}