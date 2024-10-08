using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private LayerMask layher; //I hardly know her
    
    // public bool grounded = true;
    [SerializeField] private Vector3 lastGround;
    
    bool alive = true;

    public bool invincible = false; //Fighting the urge to call this variable "the guy from fortnite"

    private void Update() {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 1, layher)){
            // Debug.Log(hit);
            lastGround = transform.position;
        }

        if (!alive)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }

    public void TakeDamage(/*int element*/){
        if(invincible){
            return;
        }
        alive = false;
    }

    public void FallRespawn(){
        transform.position = lastGround;
    }
}
