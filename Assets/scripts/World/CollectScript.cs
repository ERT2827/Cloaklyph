using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    [SerializeField] GameObject pentagon;

    saveManager SnM; //Like the Rhianna song

    [SerializeField] private int alterNumber;

    private void Start() {
        if(SnM == null){
            SnM = GameObject.Find("saveManager").GetComponent<saveManager>();
        }

        if(SnM.healthUpgrades[alterNumber]){
            pentagon.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            pentagon.SetActive(false);
            Debug.Log(alterNumber);
            SnM.IncreaseHealth(alterNumber);
        }
    }

    
}
