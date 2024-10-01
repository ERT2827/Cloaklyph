using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadZone : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    [SerializeField] private Transform enterZone; //The game will translate you here at the start of the next scene

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
        }
    }
}
