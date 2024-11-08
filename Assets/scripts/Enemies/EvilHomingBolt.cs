using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilHomingBolt : MonoBehaviour
{
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int elementAlignment;
    Vector3 targetPos;
    Transform playerTras;
    
    private void Awake() {
        playerTras = GameObject.Find("Player").transform;
        Shoot();
        // Debug.Log("I live");
    }
    
    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, playerTras.position, projectileSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other) {
        PlayerHealth PHP = other.gameObject.GetComponent<PlayerHealth>();
        
        if(other.gameObject.tag == "Player" && PHP != null){
            Debug.Log(PHP);
            PHP.TakeDamage(elementAlignment);
        }

        if (other.gameObject.tag == "Targetable" || other.gameObject.tag == "Enemy_Shield"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
        
        if(other.gameObject.tag != "Targetable"){
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }
    
    public void Shoot(){
        transform.LookAt(GameObject.Find("Player").transform);
        targetPos = playerTras.transform.position;

        StartCoroutine(Solidify());
        StartCoroutine(AutoDestruct());
    }

    IEnumerator Solidify(){
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<Collider>().enabled = true;
    }

    IEnumerator AutoDestruct(){
        yield return new WaitForSeconds(4f);

        Destroy(gameObject);
    }
}
