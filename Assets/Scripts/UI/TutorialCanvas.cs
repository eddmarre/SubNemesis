using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubNemesis.UI
{
    public class TutorialCanvas : MonoBehaviour
    {
        void Awake()
        {
            Time.timeScale = 0;
        }

        private void Update()
        {
            Cursor.lockState = CursorLockMode.None;

        }

        void OnDisable()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}