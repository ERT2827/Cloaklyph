using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airbust : MonoBehaviour
{
    private void Start() {
        StartCoroutine(SelfDestruct());
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag != "Targetable"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.tag != "Targetable"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
    }

    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(0.7f);

        Destroy(transform.parent.gameObject);
    }
}
