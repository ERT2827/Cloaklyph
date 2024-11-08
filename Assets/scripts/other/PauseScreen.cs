using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    bool isPaused = false;

    void Update()
    {
        // Toggle pause when the player presses the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Quit(){
        Application.Quit();
    }
}
