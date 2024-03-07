using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    // Start is called before the first frame update
   
    public GameObject pauseMenuPanel; // Reference to your pause menu panel

    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        pauseMenuPanel.SetActive(true); // Show the pause menu
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        pauseMenuPanel.SetActive(false); // Hide the pause menu
    }
}

