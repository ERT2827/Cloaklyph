using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    [SerializeField] private int damage;
    public Transform playerTrans;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    private void Update() {
        if (playerTrans != null){
            transform.position = playerTrans.position;
            transform.rotation = playerTrans.rotation;
        }
    }

    private void OnTriggerEnter(Collider other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();

        if(other.gameObject.tag == "Targetable" && EHP != null){
            EHP.TakeDamage(damage, 2);
        }
    }

    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(0.7f);

        Destroy(gameObject);
    }
}
