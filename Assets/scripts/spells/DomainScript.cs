using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DomainScript : MonoBehaviour
{
    public SpellController SC;
    

    private void OnTriggerStay(Collider other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();

        if(other.gameObject.tag == "Targetable" && EHP != null){
            EHP.TakeDamage(2, 3);
        }
    }

    public IEnumerator SelfDestruct(){
        SC.inGarden = true;

        yield return new WaitForSeconds(8f);

        SC.inGarden = false;
        Destroy(gameObject);
    }
}
