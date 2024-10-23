using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private saveManager SM;
    
    public void loadCombat(){
        SceneManager.LoadScene("combatTest", LoadSceneMode.Single);
    }

    public void loadMovement(){
        SceneManager.LoadScene("movementTest", LoadSceneMode.Single);
    }

    public void LoadEndless(){
        SceneManager.LoadScene("Endless", LoadSceneMode.Single);
    }

    public void LoadCampaign(){
        SceneManager.LoadScene("Hub", LoadSceneMode.Single);
    }

    public void ResetButton(){
        SM.resetprogress();
    }

    public void QuitButton(){
        Application.Quit();
    }
}
