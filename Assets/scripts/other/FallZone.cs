using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerHealth>().FallRespawn();
        }else if(other.gameObject.tag == "Targetable"){
            EnemyHealth EH = other.gameObject.GetComponent<EnemyHealth>();
            if(EH != null){
                EH.TakeDamage(9999);
            }
        }
    }
}
