using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIzardStatue : MonoBehaviour
{
    private void Update() {
        if(UniversalVariables.playerState == PlayerState.Combat){
            gameObject.tag = "blimbleWimble"; //This tag is a place holder for when a tag isn't assigned
        }else{
            gameObject.tag = "Targetable";
        }
    }
}
