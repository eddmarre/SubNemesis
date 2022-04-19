using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamageAmount = 10f;

    private void OnValidate()
    {
        GetComponent<SphereCollider>().radius = .5f;
    }

    private void Start()
    {
        GetComponent<SphereCollider>().radius = .5f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Health>())
        {
            if (collision.gameObject.GetComponent<Health>().onTakeDamageAction != null)
            {
                collision.gameObject.GetComponent<Health>().onTakeDamageAction.Invoke(bulletDamageAmount);
                Destroy(gameObject);
            }
        }
    }
}