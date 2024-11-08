using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellDoor : MonoBehaviour
{
    [SerializeField] private string spellCheck;
    bool unlocked = false;
    
    [Header("UX")]
    [SerializeField] private Sprite doorImage;
    [SerializeField] private GameObject smallPic;
    [SerializeField] private GameObject bigPic;

    [Header("Movement variables")]
    [SerializeField] private Vector3 startingpos;
    [SerializeField] private Vector3 endpos;
    
    private void Start() {
        smallPic.GetComponent<Image>().sprite = doorImage;
        
        bigPic.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = doorImage;

        startingpos = transform.position;
        endpos = transform.position + new Vector3(0, -25f, 0);
    }

    private void Update() {
        if(unlocked){
            transform.position = Vector3.Lerp(transform.position, endpos, Time.deltaTime * 0.3f);
        }

        if(transform.position == endpos){
            gameObject.SetActive(false);
        }
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

    public void SpellChecker(string comper){
        if(comper.Contains(spellCheck)){
            unlocked = true;
            // Destroy(transform.GetChild(0).gameObject); 
        }
    }
}
