using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIManagerTafe
{
    public class DeathMenu : MonoBehaviour
    {
        private void Awake()
        {
            Time.timeScale = 1;
        }
        // Used when starting a new game.
        public void NewGame()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
        // Used when respawing.
        public void Respawn()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }

        // Used when quiting the game.
        public void QuitButton()
        {
            Time.timeScale = 1;

            Application.Quit();
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}