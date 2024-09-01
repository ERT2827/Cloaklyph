using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDisk : MonoBehaviour
{
    GameObject[] Targets;
    
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectileDamage;
    [SerializeField] private int MaxBounces;
    int Bounces;

    Transform targetPos;

    // Start is called before the first frame update
    void Awake()
    {
        Targets = GameObject.FindGameObjectsWithTag("Targetable");

        targetPos = FindClosest();
        Bounces = 1;
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
        if(Bounces >= MaxBounces){
            Destroy(gameObject);
        }
        
        Bounces += 1;
        targetPos = FindClosest();
    }

    Transform FindClosest(){
        Transform tempclosest = null;
        float closestDistance = Mathf.Infinity;

        foreach(GameObject i in Targets){
            if(i != null){
                float d = Vector3.Distance(i.transform.position, transform.position);
                if(d < closestDistance){
                    tempclosest = i.transform;
                    closestDistance = d;
                }
            }
        }

        return tempclosest;
    }
}
