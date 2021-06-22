using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIManagerTafe
{
    public class MenuManager : MonoBehaviour
    {
        public GameObject MainOverlay;
        public GameObject PauseMenu;
        public GameObject KeyBinds;

        // Used when pausing the game.
        public void PauseButton()
        {
            Time.timeScale = 0;
            PauseMenu.gameObject.SetActive(true);
            MainOverlay.gameObject.SetActive(false);
            KeyBinds.gameObject.SetActive(false);
        }

        // Used when Resuming the game.
        public void ResumeButton()
        {
            Time.timeScale = 1;
            MainOverlay.gameObject.SetActive(true);
            PauseMenu.gameObject.SetActive(false);
            KeyBinds.gameObject.SetActive(false);
        }

        // Used when opening keybinds.
        public void KeybindsButton()
        {
            Time.timeScale = 0;
            KeyBinds.gameObject.SetActive(true);
            PauseMenu.gameObject.SetActive(false);
            MainOverlay.gameObject.SetActive(false);
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