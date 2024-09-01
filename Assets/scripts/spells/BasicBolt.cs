using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBolt : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] int projectileDamage;
    Vector3 targetPos;
    
    private void Awake() {
        Debug.Log("I live");
        StartCoroutine(AutoDestruct());
    }
    
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        EnemyHealth EHP = other.GetComponent<EnemyHealth>();
        
        if(other.gameObject.tag == "Targetable" && EHP != null){
            Debug.Log(EHP);
            EHP.TakeDamage(projectileDamage);
        }
        
        if(other.gameObject.tag != "Player"){
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
    
    public void Shoot(Transform Target){
        if(Target != null){
            targetPos = Target.position; 
            transform.LookAt(Target);
        }else{
            Destroy(gameObject);
        }
    }

    IEnumerator AutoDestruct(){
        yield return new WaitForSeconds(7f);

        Destroy(gameObject);
    }
    
}
