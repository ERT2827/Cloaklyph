using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private float projectileDuration;
    [SerializeField] private int elementAlignment;
    Vector3 targetPos;
    Transform playerTrans;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTrans = GameObject.Find("Player").transform;


        
        Quaternion lookTarget = Quaternion.LookRotation(playerTrans.position - transform.position) ;
        // Quaternion q = Quaternion.Euler(90, lookTarget.y, 0);
        transform.rotation = lookTarget;

        StartCoroutine(SelfDestruct());
    }

    private void OnCollisionEnter(Collision other) {
        PlayerHealth PHP = other.gameObject.GetComponent<PlayerHealth>();
        
        if(other.gameObject.tag == "Player" && PHP != null){
            Debug.Log(PHP);
            PHP.TakeDamage(elementAlignment);
        }

        if (other.gameObject.tag == "Targetable"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
        
        if(other.gameObject.tag != "Targetable"){
            Debug.Log(other.gameObject.name);
            Destroy(gameObject);
        }
    }

    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(projectileDuration);

        Destroy(gameObject);
    }
}
