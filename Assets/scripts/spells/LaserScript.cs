using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float coolDown = 0.1f;
    float coolDownTimer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
        
    }

    private void Update() {
        coolDownTimer+= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();

        if(other.gameObject.tag == "Targetable" && EHP != null && coolDownTimer > coolDown){
            EHP.TakeDamage(damage, 2);
            coolDownTimer = 0;
        }
    }

    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }
}
