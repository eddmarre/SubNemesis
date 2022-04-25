using System;
using SubNemesis.UI;
using UnityEngine;

namespace SubNemesis.GamePlay
{

    public class FindGUIs : MonoBehaviour
    {
         private GameObject pauseCanvas;
         private GameObject deathCanvas;
         private GameObject waveCanvas;

         private void Awake()
         {
             waveCanvas = FindObjectOfType<WaveCanvas>().gameObject;
             deathCanvas = FindObjectOfType<DeathCanvas>().gameObject;
             pauseCanvas = FindObjectOfType<PauseCanvas>().gameObject;
         }

         private void Start()
         {
             pauseCanvas.SetActive(false);
             deathCanvas.SetActive(false);
         }

         public void TogglePause()
         {
             pauseCanvas.SetActive(!pauseCanvas.activeSelf);
             if (pauseCanvas.activeSelf)
             {
                 Time.timeScale = 0;
             }
             else
             {
                 Time.timeScale = 1;
             }
         }

         public void GameOver()
         {
             deathCanvas.SetActive(true);
             waveCanvas.SetActive(false);
         }
    }
}