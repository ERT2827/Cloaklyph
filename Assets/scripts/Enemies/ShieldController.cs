using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private Transform owner;

    private void Start() {
        owner = transform.parent;

        if(owner.GetComponent<HammerTime>() != null) {
            owner.GetComponent<HammerTime>().shield = gameObject;
        }else if(owner.parent.GetComponent<WizardScript>() != null){
            owner.parent.GetComponent<WizardScript>().wizShield = gameObject;
        }

        transform.SetParent(null);

        if(owner.parent.GetComponent<WizardScript>() != null){
            gameObject.SetActive(true);
        }
    }

    private void Update() {
        if(owner == null){
            Destroy(gameObject);   
        }
        
        transform.position = owner.position;
        transform.rotation = owner.rotation;
    }
}
