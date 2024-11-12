using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMelee : MonoBehaviour
{
    [SerializeField] private Rigidbody ysl; //Respect the classics
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private int element;
    
    // Start is called before the first frame update
    void Start()
    {
        ysl = gameObject.GetComponent<Rigidbody>();

        StartCoroutine(AutoDestruct());

        ysl.velocity = speed * transform.forward;
    }

    private void OnTriggerEnter(Collider other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();

        if(other.gameObject.tag == "Targetable" && EHP != null){
            EHP.TakeDamage(damage, 3);
        }
    }

    IEnumerator AutoDestruct(){
        yield return new WaitForSeconds(7f);

        Destroy(gameObject);
    }
}
