using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBolt : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] int projectileDamage;
    [SerializeField] int element;
    [SerializeField] float lifeTime;

    GameObject[] Targets;

    Vector3 targetPos;
    
    private void Awake() {
        Debug.Log("I live");
        StartCoroutine(AutoDestruct());
        StartCoroutine(Solidify());
    }

    private void Start() {
        Targets = GameObject.FindGameObjectsWithTag("Targetable");
        Shoot();
    }
    
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);

        if(transform.position == targetPos){
            targetPos = transform.forward * 100; 
        }
    }

    private void OnCollisionEnter(Collision other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();
        
        if(other.gameObject.tag == "Player"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
        
        if(other.gameObject.tag == "Targetable" && EHP != null){
            // Debug.Log(EHP);
            EHP.TakeDamage(projectileDamage, element);
        }
        
        if(other.gameObject.tag != "Player"){
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
    
    void Shoot(){
        Transform Target = FindFarthest();

        if(Target != null){
            transform.LookAt(Target);
            targetPos = Target.position; 
        }else{
            Destroy(gameObject);
        }
    }

    Transform FindFarthest(){
        Transform tempfarthest = null;
        float farthestDistance = 0;

        foreach(GameObject i in Targets){
            if(i != null){
                float d = Vector3.Distance(i.transform.position, transform.position);
                if(d > farthestDistance){
                    tempfarthest = i.transform;
                    farthestDistance = d;
                }
            }
        }

        return tempfarthest;
    }

    IEnumerator Solidify(){
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<Collider>().enabled = true;
    }
    
    IEnumerator AutoDestruct(){
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}