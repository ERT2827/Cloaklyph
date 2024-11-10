using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    public string noteBookText;
    [SerializeField] TMP_InputField noteBook;
    [SerializeField] TMP_Text textDisplay;
    bool isPaused = false;
    saveManager saveManager;

    private void Start() {
        saveManager = GameObject.Find("saveManager").GetComponent<saveManager>();
    }
    
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
        noteBookText = noteBook.text;
        saveManager.SetNoteBook(noteBookText);
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;
    }

    public void MainMenu(){
        noteBookText = noteBook.text;
        saveManager.SetNoteBook(noteBookText);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Quit(){
        noteBookText = noteBook.text;
        saveManager.SetNoteBook(noteBookText);
        Application.Quit();
    }

    public void SetNotes(string notes){
        noteBookText = notes;
        // Debug.Log("NBT is " + noteBookText);
        noteBook.text = notes;
    }
    // public void SetNotes(){
    //     noteBook.text = "Piss mode fr fr";
    // }
}
