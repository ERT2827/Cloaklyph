using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysBolt : MonoBehaviour
{
    [SerializeField] private Rigidbody ysl; //Respect the classics
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int element;
    // Start is called before the first frame update
    void Start()
    {
        ysl = gameObject.GetComponent<Rigidbody>();
        
        StartCoroutine(Solidify());
        StartCoroutine(AutoDestruct());

        ysl.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();
        
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Player_Proj"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
        
        if(other.gameObject.tag == "Targetable" && EHP != null){
            // Debug.Log(EHP);
            EHP.TakeDamage(damage, element);
        }
        
        if(other.gameObject.tag != "Player" || other.gameObject.tag != "Player_Proj"){
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Targetable" || other.gameObject.tag == "Enemy_Proj"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
    }

    IEnumerator Solidify(){
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<Collider>().enabled = true;
    }

    IEnumerator AutoDestruct(){
        yield return new WaitForSeconds(7f);

        Destroy(gameObject);
    }
}
