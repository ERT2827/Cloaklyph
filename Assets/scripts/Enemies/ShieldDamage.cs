using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDamage : MonoBehaviour
{
    [SerializeField] private int elementAlignment;
    
    private void OnCollisionEnter(Collision other) {
        PlayerHealth PHP = other.gameObject.GetComponent<PlayerHealth>();
        
        if(other.gameObject.tag == "Player" && PHP != null){
            Debug.Log(PHP);
            PHP.TakeDamage(elementAlignment);
        }

        if (other.gameObject.tag == "Targetable" || other.gameObject.tag == "Enemy_Proj"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
        
    }
}
