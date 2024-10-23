using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardScript : MonoBehaviour
{
    
    [Header("Movement and animation")]
    [SerializeField] private GameObject wizhardModal;
    [SerializeField] private float rotationSpeed = 1;

    bool wizardActive = false;

    bool moving;

    [Header("Attacks")]
    [SerializeField] private float attackDuration = 1;
    [SerializeField] private float cooldownTime = 1;
    [SerializeField] private GameObject[] projectiles;

    public float cooldownTimer = 0;

    private void Start() {
        wizhardModal = transform.GetChild(0).gameObject;

        wizhardModal.SetActive(false);
    }
    
    private void Update() {

        if(wizardActive){
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer > cooldownTime && moving){
                StartCoroutine(StopAndAttack());
            }

            if(moving){
                transform.Rotate(0, (rotationSpeed * Time.deltaTime), 0);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !wizardActive){
            wizhardModal.SetActive(true);
            StartCoroutine(StopAndAttack());
            wizardActive = true;
        }
    }

    IEnumerator StopAndAttack(){

        Debug.Log("Attacking");
        moving = false;

        //Attack function
        GameObject bolt = Instantiate(projectiles[0], wizhardModal.transform.position, Quaternion.identity) as GameObject;

        yield return new WaitForSeconds(attackDuration);

        moving = true;
        cooldownTimer = 0;

    }
}
