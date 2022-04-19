using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    private void Update()
    {
        GameObject[] _children;
        _children = GetComponentsInChildren<GameObject>();
        gameObject.name = _children.Length.ToString();
    }
}
