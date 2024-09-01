using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellCube : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.5f;
    [SerializeField] private int projectileDamage = 0;
    bool dangerous;

    void Start (){
        StartCoroutine(fallTime());
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Targetable" && dangerous){
            EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();
        
            if(other.gameObject.tag == "Targetable" && EHP != null){
                // Debug.Log(EHP);
                EHP.TakeDamage(projectileDamage);
            }
        }else if (other.gameObject.layer == 6){
            StartCoroutine(deleteSelf());
            // I should maybe make this change colour when it's safe
            dangerous = false;
        }
    }

    IEnumerator fallTime(){
        yield return new WaitForSeconds(fallDelay);

        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    IEnumerator deleteSelf(){
        yield return new WaitForSeconds(2);

        Destroy(gameObject);    
    }

    
}
