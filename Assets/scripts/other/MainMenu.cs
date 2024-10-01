using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void loadCombat(){
        SceneManager.LoadScene("combatTest", LoadSceneMode.Single);
    }

    public void loadMovement(){
        SceneManager.LoadScene("movementTest", LoadSceneMode.Single);
    }

    public void LoadEndless(){
        SceneManager.LoadScene("Endless", LoadSceneMode.Single);
    }
}
