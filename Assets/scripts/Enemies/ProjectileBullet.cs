using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody ysl; //Respect the classics
    [SerializeField] private float speed;
    [SerializeField] private int elementAlignment;
    // Start is called before the first frame update
    void Start()
    {
        ysl = gameObject.GetComponent<Rigidbody>();
        
        StartCoroutine(Solidify());
        StartCoroutine(AutoDestruct());

        ysl.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision other) {
        PlayerHealth PHP = other.gameObject.GetComponent<PlayerHealth>();
        
        if(other.gameObject.tag == "Player" && PHP != null){
            Debug.Log(PHP);
            PHP.TakeDamage(elementAlignment);
        }

        if (other.gameObject.tag == "Targetable" || other.gameObject.tag == "Enemy_Proj"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
        
        if(other.gameObject.tag != "Targetable" || other.gameObject.tag != "Enemy_Proj"){
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag == "Targetable" || other.gameObject.tag == "Enemy_Proj"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
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
