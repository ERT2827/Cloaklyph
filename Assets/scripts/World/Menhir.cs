using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menhir : MonoBehaviour
{
    [SerializeField] private Sprite menhirImage;
    [SerializeField] private GameObject smallPic;
    [SerializeField] private GameObject bigPic;
    
    private void Start() {
        smallPic.transform.GetChild(0).GetComponent<Image>().sprite = menhirImage;
        
        bigPic.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = menhirImage;
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && UniversalVariables.playerState != PlayerState.Combat){
            smallPic.SetActive(false);
            bigPic.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            smallPic.SetActive(true);
            bigPic.SetActive(false);
        }
    }
}
