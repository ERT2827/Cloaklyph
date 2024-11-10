using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fizzle_Script : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int element;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestruct());
    }

    private void OnTriggerEnter(Collider other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();
        
        if(other.gameObject.tag == "Targetable" && EHP != null){
            // Debug.Log(EHP);
            EHP.TakeDamage(damage, element);
        }
        
        if(other.gameObject.tag == "Enemy_Proj"){
            Destroy(other.gameObject);
        }
    }

    IEnumerator AutoDestruct(){
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
