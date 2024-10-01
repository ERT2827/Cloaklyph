using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private GameObject player; //It needs the player object to
    //add it to the list of targets

    private void Awake() {
        currentHealth = maxHealth;
    }

    private void Start() {
        player = GameObject.Find("Player");
        player.GetComponent<SpellController>().AddTargets();
    }

    private void Update() {
        if (currentHealth <= 0){
            // Tell the player health your element
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int DT){ //DT = Damage Taken
        currentHealth -= DT;
        HitAnimation();
    }

    void HitAnimation(){
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();

        if(gameObject.GetComponent<SlugScript>() != null){
            transform.position = transform.position + new Vector3(0, 1f, 0);
        }else if(agent != null){
            Debug.Log("Hi pooky");
            agent.Warp(transform.position + new Vector3(0, 4f, 0));
        }
        
    }
}
