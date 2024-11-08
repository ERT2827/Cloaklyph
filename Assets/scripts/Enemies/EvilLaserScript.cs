using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilLaserScript : MonoBehaviour
{
    [SerializeField] private int alignment;
    public float upTime = 5f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    private void OnTriggerEnter(Collider other) {
        PlayerHealth PHP = other.gameObject.GetComponent<PlayerHealth>();

        if(other.gameObject.tag == "Player" && PHP != null){
            
            PHP.TakeDamage(alignment);
        }

        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
    }

    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(upTime);

        Destroy(gameObject);
    }
}
