using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeButEvil : MonoBehaviour
{
    [SerializeField] private float fallDelay = 0.1f;
    bool dangerous = true;

    void Start (){
        StartCoroutine(fallTime());
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player" && dangerous){
            PlayerHealth PHP = other.gameObject.GetComponent<PlayerHealth>();
        
            if(other.gameObject.tag == "Player" && PHP != null){
                // Debug.Log(EHP);
                PHP.TakeDamage(3);
                Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
            }
        }else if (other.gameObject.layer == 6){
            StartCoroutine(deleteSelf());
            // I should maybe make this change colour when it's safe
            dangerous = false;
        }
    }

    IEnumerator fallTime(){
        yield return new WaitForSeconds(fallDelay);

        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    IEnumerator deleteSelf(){
        yield return new WaitForSeconds(2);

        Destroy(gameObject);    
    }
}
