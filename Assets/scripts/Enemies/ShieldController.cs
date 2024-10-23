using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private Transform owner;
    [SerializeField] private int elementAlignment;

    private void Start() {
        owner = transform.parent;

        if(owner.GetComponent<HammerTime>() != null) {
            owner.GetComponent<HammerTime>().shield = gameObject;
        }

        transform.SetParent(null);
    }

    private void Update() {
        if(owner == null){
            Destroy(gameObject);   
        }
        
        transform.position = owner.position;
        transform.rotation = owner.rotation;
    }

    private void OnCollisionEnter(Collision other) {
        PlayerHealth PHP = other.gameObject.GetComponent<PlayerHealth>();
        
        if(other.gameObject.tag == "Player" && PHP != null){
            Debug.Log(PHP);
            // PHP.TakeDamage(elementAlignment);
        }

        if (other.gameObject.tag == "Targetable"){
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), other.gameObject.GetComponent<Collider>());
        }
        
    }
}
