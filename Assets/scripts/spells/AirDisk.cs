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

    Transform lastTarget;

    // Start is called before the first frame update
    void Awake()
    {
        Targets = GameObject.FindGameObjectsWithTag("Targetable");

        targetPos = FindClosest();
        StartCoroutine(Solidify());
        Bounces = 1;
    }

    private void Update() {
        if(targetPos != null){
            transform.position = Vector3.MoveTowards(transform.position, targetPos.position, projectileSpeed * Time.deltaTime);
        }else{
            bounce();
        }
    }

    private void OnCollisionEnter(Collision other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();
        
        if(other.gameObject.tag == "Targetable" && EHP != null){
            // Debug.Log(EHP);
            EHP.TakeDamage(projectileDamage);
            targetPos = null;
        }

        if(other.gameObject.layer == 7 || other.gameObject.layer == 6){
            bounce();
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
                    if(i.transform != lastTarget){
                        tempclosest = i.transform;
                        closestDistance = d;
                    }
                }
            }
        }

        lastTarget = tempclosest;
        return tempclosest;
    }

    IEnumerator Solidify(){
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<Collider>().enabled = true;
    }
}
