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

    private void OnCollisionEnter(Collision other) {
        PlayerHealth PHP = other.gameObject.GetComponent<PlayerHealth>();
        
        if(other.gameObject.tag == "Player" && PHP != null){
            Debug.Log(PHP);
            PHP.TakeDamage();
        }

        if (other.gameObject.tag == "Targetable"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
        
        if(other.gameObject.tag != "Targetable"){
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
    
    public void Shoot(){
        transform.LookAt(GameObject.Find("Player").transform);
        targetPos = transform.rotation * new Vector3(0, 0, 100); 
        targetPos.y = 1;

        StartCoroutine(Solidify());
        StartCoroutine(AutoDestruct());
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
