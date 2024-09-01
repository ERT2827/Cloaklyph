using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseProjectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private string projectileElement;
    Vector3 targetPos;
    
    private void Awake() {
        Shoot();
        // Debug.Log("I live");
    }
    
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, projectileSpeed * Time.deltaTime);

        if (transform.position == targetPos) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        PlayerHealth PHP = other.GetComponent<PlayerHealth>();
        
        if(other.gameObject.tag == "Player" && PHP != null){
            Debug.Log(PHP);
            PHP.TakeDamage();
        }
        
        if(other.gameObject.tag != "Targetable"){
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
    
    public void Shoot(){
        targetPos = GameObject.Find("Player").transform.position;

        StartCoroutine(AutoDestruct());
    }

    IEnumerator AutoDestruct(){
        yield return new WaitForSeconds(7f);

        Destroy(gameObject);
    }
}
