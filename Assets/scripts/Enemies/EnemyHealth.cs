using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;

    private void Awake() {
        currentHealth = maxHealth;
    }

    private void Update() {
        if (currentHealth <= 0){
            // Tell the player health your element
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int DT){ //DT = Damage Taken
        currentHealth -= DT;
    }
}
