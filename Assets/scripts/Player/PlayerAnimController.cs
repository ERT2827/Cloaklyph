using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject[] arms;
    Transform spawnPoint;

    private void Start() {
        if (playerController == null){
            playerController = gameObject.GetComponent<PlayerController>();
            spawnPoint = transform.GetChild(3);
        }
    }


    private void Update() {
        
        //Anim Spawners
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            Quaternion q = Quaternion.Euler(0, 0, 0);
        
            GameObject melee = Instantiate(arms[0], spawnPoint.position, q) as GameObject;
            melee.transform.SetParent(gameObject.transform);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            Quaternion q = Quaternion.Euler(0, 0, 0);
        
            GameObject melee = Instantiate(arms[1], spawnPoint.position, q) as GameObject;
            melee.transform.SetParent(gameObject.transform);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            Quaternion q = Quaternion.Euler(0, 0, 0);
        
            GameObject melee = Instantiate(arms[2], spawnPoint.position, q) as GameObject;
            melee.transform.SetParent(gameObject.transform);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            Quaternion q = Quaternion.Euler(0, 0, 0);
        
            GameObject melee = Instantiate(arms[3], spawnPoint.position, q) as GameObject;
            melee.transform.SetParent(gameObject.transform);
        }
        
    }
}
