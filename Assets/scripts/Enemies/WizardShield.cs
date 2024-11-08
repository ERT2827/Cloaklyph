using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardShield : MonoBehaviour
{
    [SerializeField] private Transform owner;

    private void Start() {
        owner = transform.parent;

        owner.parent.GetComponent<WizardScript>().wizShield = gameObject;

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
