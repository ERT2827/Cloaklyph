using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int alignment;
    [SerializeField] private int weakness;

    [Header("Other Systems")]
    [SerializeField] private GameObject[] hitEffects = new GameObject[4];

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
        if(transform.position.y < -10f){
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int DT, int EA){ //DT = Damage Taken, EA = Elemental Alignment
        if (EA == weakness){
            currentHealth -= DT * 2;
            Instantiate(hitEffects[EA], transform.position, Quaternion.identity);
            Instantiate(hitEffects[EA], transform.position, Quaternion.identity);
            Instantiate(hitEffects[EA], transform.position, Quaternion.identity);
            // Debug.Log("Crit on " + gameObject.name);
        }else if(EA == alignment){
            currentHealth -= DT / 2;
            Instantiate(hitEffects[EA], transform.position, Quaternion.identity);
            Instantiate(hitEffects[EA], transform.position, Quaternion.identity);
            // Debug.Log("Resist hit on " + gameObject.name);
        }else{
            currentHealth -= DT;
            Instantiate(hitEffects[EA], transform.position, Quaternion.identity);
            // Debug.Log("Normal Damage on " + gameObject.name);
        }


        if (currentHealth <= 0){
            if(alignment <= 2){
                player.GetComponent<PlayerHealth>().KillHeal(alignment);
            }
            Destroy(gameObject);
        }
        
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

    public int GetHealth(){
        return currentHealth;
    }
}
