using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDiskDeluxe : MonoBehaviour
{
    GameObject[] Targets;
    
    GameObject previousTarget;
    
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;
    [SerializeField] private float bounceDuration;
    int Bounces;

    Transform targetPos;

    // Start is called before the first frame update
    void Awake()
    {
        Targets = GameObject.FindGameObjectsWithTag("Targetable");

        targetPos = FindClosest();
        StartCoroutine(SelfDestruct());
        StartCoroutine(Solidify());
    }

    private void Update() {
        if(targetPos != null){
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, projectileSpeed * Time.deltaTime);
        }else{
            bounce();
        }
    }

    private void OnTriggerEnter(Collider other){
        EnemyHealth EHP = other.GetComponent<EnemyHealth>();
        
        if(other.gameObject.tag == "Targetable" && EHP != null){
            // Debug.Log(EHP);
            EHP.TakeDamage(projectileDamage);
            targetPos = null;
        }
    }

    void bounce(){
        targetPos = FindClosest();
    }

    Transform FindClosest(){
        Transform tempclosest = null;
        float closestDistance = Mathf.Infinity;

        foreach(GameObject i in Targets){
            if(i != null && i != previousTarget){
                float d = Vector3.Distance(i.transform.position, transform.position);
                if(d < closestDistance){
                    tempclosest = i.transform;
                    closestDistance = d;
                    previousTarget = i;
                }
            }
            
        }

        // Debug.Log(previousTarget);

        return tempclosest;
    }

    IEnumerator Solidify(){
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<Collider>().enabled = true;
    }

    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(bounceDuration);

        Destroy(gameObject);   
    }
}
