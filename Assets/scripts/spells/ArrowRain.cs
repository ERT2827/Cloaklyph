using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRain : MonoBehaviour
{
    [SerializeField] private int projectileDamage = 0;
    
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Targetable"){
            EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();
        
            if(other.gameObject.tag == "Targetable" && EHP != null){
                // Debug.Log(EHP);
                EHP.TakeDamage(projectileDamage, 1);
            }
        }else if (other.gameObject.layer == 6){
            Destroy(gameObject);
        }
    }
}
