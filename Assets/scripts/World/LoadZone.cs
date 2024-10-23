using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadZone : MonoBehaviour
{
    [SerializeField] private string nextSceneName;

    [SerializeField] private Vector3 enterZone; //The game will translate you here at the start of the next scene

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            saveManager SM = GameObject.Find("saveManager").GetComponent<saveManager>();

            SM.LoadNewLevel(nextSceneName, enterZone);
        }
    }
}
