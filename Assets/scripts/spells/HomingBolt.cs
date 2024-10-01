using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBolt : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] int projectileDamage;
    Transform homingTarget;
    
    private void Awake() {
        Debug.Log("I live");
        StartCoroutine(AutoDestruct());
        StartCoroutine(Solidify());
    }
    
    private void Update() {
        if(homingTarget == null){
            Destroy(gameObject);
        }
        transform.position = Vector3.MoveTowards(transform.position, homingTarget.position, projectileSpeed * Time.deltaTime);
        transform.LookAt(homingTarget);
    }

    private void OnCollisionEnter(Collision other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();
        
        if(other.gameObject.tag == "Player"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }

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
            homingTarget = Target;
        }else{
            Destroy(gameObject);
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
