using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SubNemesis.UI
{
    public class ChangeScenes : MonoBehaviour
    {
        private SceneManager _sceneManager;

        public void LoadGame()
        {
            try
            {
                FindObjectOfType<UbhObjectPool>().ReleaseAllBullet();
            }
            catch (Exception e)
            {
                String nothing = e.ToString();
            }

            SceneManager.LoadScene("Coral Reef");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Intro");
        }
    }
}