using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDoor_Child : MonoBehaviour
{
    [SerializeField] SpellDoor parentDoor;
    PlayerState ps;
    SpellController spellController;
    SpellDoor daddyDoor;

    private void Start() {
        spellController = GameObject.Find("Player").GetComponent<SpellController>();
        daddyDoor = transform.parent.gameObject.GetComponent<SpellDoor>();
    }

    private void FixedUpdate() {
        if(UniversalVariables.playerState != PlayerState.Combat){
            gameObject.tag = "Targetable";
        }else{
            gameObject.tag = "blimbleWimble";
        }

        if(ps != UniversalVariables.playerState){
            spellController.AddTargets();
            ps = UniversalVariables.playerState;
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        //Tell the parent door what kind of spell it was
    }
}
