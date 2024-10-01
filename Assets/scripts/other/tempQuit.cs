using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempQuit : MonoBehaviour
{
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(SceneManager.GetActiveScene().name != "mainMenu"){
                SceneManager.LoadScene("mainMenu", LoadSceneMode.Single);
            }else{
                Application.Quit();
            }
        }

        if(Input.GetKeyDown(KeyCode.R)){
            // Debug.Log("Restart");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}
