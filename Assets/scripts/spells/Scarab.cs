using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarab : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets = new List<GameObject>(); 
    
    [Header("Movement")]
    Transform currentTarget;
    [SerializeField] float ScarabSpeed = 10;

    [Header("Attack")]

    [SerializeField] private float UpTime = 30;
    [SerializeField] private int damage = 1;
    [SerializeField] private float coolDown = 1;
    float coolDownTimer;


    private void Start() {
        AddTargets();

        currentTarget = SetTarget();
    }

    private void Update() {
        coolDownTimer += Time.deltaTime;

        if(targets.Count > 0) {
            if(currentTarget == null){
            currentTarget = SetTarget();
            }else{
                transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, ScarabSpeed * Time.deltaTime);
                transform.LookAt(currentTarget);
            }
        }

        
    }

    private void FixedUpdate() {
        CheckTargets();

        if(targets.Count <= 0){
            AddTargets();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
    }

    private void OnCollisionStay(Collision other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();

        if(other.gameObject.tag == "Targetable" && EHP != null && coolDownTimer > coolDown){
            EHP.TakeDamage(damage);
            coolDownTimer = 0;
        }
    }

    void CheckTargets(){
        foreach (var i in targets)
        {
            if(i == null){
                targets.Remove(i);
                return;
            }
        }
    }


    Transform SetTarget(){
        Transform tempclosest = null;
        float closestDistance = Mathf.Infinity;

        foreach(GameObject i in targets){
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

    public void AddTargets(){
        GameObject[] temp_Targets = GameObject.FindGameObjectsWithTag("Targetable"); //I was forced to change this because this doesn't allow me to add new game objects
        foreach (GameObject i in temp_Targets){
            if(!targets.Contains(i)){
                targets.Add(i);
            }
        }
    }

    IEnumerator AutoDestruct(){
        yield return new WaitForSeconds(UpTime);

        Destroy(gameObject);
    }
}
