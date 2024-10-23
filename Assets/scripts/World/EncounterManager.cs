using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    [SerializeField] private float exitCooldown = 10f;
    [SerializeField] private List<GameObject> minions = new List<GameObject>();

    [SerializeField] private bool combatComplete = false;
    
    private void Start() {
        int childcount = transform.childCount;

        for (int i = 0; i < childcount; i++)
        {
            minions.Add(transform.GetChild(i).gameObject);
        }

        foreach (GameObject i in minions)
        {
            i.SetActive(false);
        }
    }

    private void Update() {
        if(transform.childCount <= 0){
            combatComplete = true;
        }

        if (combatComplete)
        {
            UniversalVariables.playerState = PlayerState.Exploring;
            gameObject.SetActive(false);
        }
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !combatComplete){
            UniversalVariables.playerState = PlayerState.Combat;

            foreach (GameObject i in minions)
            {
                if(i == null){
                    minions.Remove(i);
                }else{
                    i.SetActive(true);
                }
            }

            other.gameObject.GetComponent<SpellController>().AddTargets();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player"){
            StartCoroutine(ExitTimer());
        }
    }

    IEnumerator ExitTimer(){
        yield return new WaitForSeconds(exitCooldown);

        foreach (GameObject i in minions)
        {
            if(i == null){
                    minions.Remove(i);
                }else{
                    i.SetActive(false);
                }
        }

        UniversalVariables.playerState = PlayerState.Exploring;
    }

}
