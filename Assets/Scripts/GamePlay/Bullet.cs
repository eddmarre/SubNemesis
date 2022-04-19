using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletDamageAmount = 10f;

    // private void OnEnable()
    // {
    //     GetComponent<Collider>().enabled = false;
    //     StartCoroutine(EnableColliderDelay());
    // }
    //
    // IEnumerator EnableColliderDelay()
    // {
    //     yield return new WaitForSeconds(.5f);
    //     GetComponent<Collider>().enabled = true;
    // }

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