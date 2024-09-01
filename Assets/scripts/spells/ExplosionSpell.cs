using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSpell : MonoBehaviour
{
    [SerializeField] private int ballDamage = 2;
    
    private void Awake() {
        StartCoroutine(SelfDelete());
    }

    private void OnCollisionEnter(Collision other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();
        
        if(other.gameObject.tag == "Targetable" && EHP != null){
            EHP.TakeDamage(ballDamage);
        }
    }

    IEnumerator SelfDelete(){
        yield return new WaitForSeconds(7f);

        Destroy(gameObject);
    }
}
