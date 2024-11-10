using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WizardScript : MonoBehaviour
{
    
    [Header("Movement and animation")]
    [SerializeField] private GameObject wizhardModal;
    [SerializeField] private GameObject decorWizard;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    [SerializeField] private float rotationSpeed = 1;

    bool wizardActive = false;
    bool fightOver = false;

    bool moving;

    [Header("Attacks")]
    [SerializeField] private float attackDuration = 1;
    [SerializeField] private float cooldownTime = 1;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private GameObject player;

    float cooldownTimer = 0;
    int attackCount = 0;
    public GameObject wizShield;

    [Header("Final Area")]
    [SerializeField] private GameObject areaHolder;
    [SerializeField] private Vector3 targetPosition;

    [Header("Health Bar")]
    [SerializeField] private GameObject healthBar;
    [SerializeField] private Slider healthBarSlide;
    [SerializeField] private EnemyHealth wizHealth;

    private void Start() {
        wizhardModal = transform.GetChild(0).gameObject;

        player = GameObject.Find("Player");

        healthBarSlide.maxValue = wizHealth.GetHealth();

        wizhardModal.SetActive(false);
        healthBar.SetActive(false);
        wizShield.SetActive(false);
    }
    
    private void Update() {

        if(wizardActive){
            cooldownTimer += Time.deltaTime;

            healthBarSlide.value = wizHealth.GetHealth();

            if (cooldownTimer > cooldownTime && moving && wizhardModal != null){
                StartCoroutine(StopAndAttack());
            }

            if(moving){
                transform.Rotate(0, (rotationSpeed * Time.deltaTime), 0);
            }
        }

        if (wizhardModal == null && fightOver == false){
            PostFight();
        }

        if(fightOver == true && areaHolder.transform.position != targetPosition){
            areaHolder.transform.position = Vector3.Lerp(areaHolder.transform.position, targetPosition, 0.2f);
            healthBar.SetActive(false);
        }


        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player" && !wizardActive){
            decorWizard.SetActive(false);
            wizhardModal.SetActive(true);
            healthBar.SetActive(true);
            StartCoroutine(StopAndAttack());
            wizardActive = true;
            wizShield.SetActive(false);
        }
    }

    void PostFight(){
        wizardActive = false;
        fightOver = true;
    }

    void SpellCaster(){
        int attackPick;
        
        if(attackCount == 0 || attackCount == 6){
            attackPick = attackCount;
        }else{
            attackPick = Random.Range(0, 6);
        }

        if(attackPick == 0){ //Purplebolt
            Instantiate(projectiles[0], wizhardModal.transform.position, Quaternion.identity);
        }else if(attackPick == 1){ //Burst fire
            StartCoroutine(BurstFire());
        }else if(attackPick == 2){ //Laser
            // Quaternion q = Quaternion.Euler(wizhardModal.transform.rotation.x, wizhardModal.transform.rotation.y - 70, wizhardModal.transform.rotation.z);

            Vector3 spawnPoint = wizhardModal.transform.position + new Vector3(0, -8f, 0);
        
            GameObject laser = Instantiate(projectiles[2], spawnPoint, Quaternion.identity) as GameObject;
            laser.transform.SetParent(gameObject.transform);
            laser.transform.localRotation = Quaternion.identity;
        }else if(attackPick == 3){ //Brick, needs to be fixed
            Vector3 rock_loc = player.transform.position + new Vector3(0, 5, 0);
        
            GameObject rock = Instantiate(projectiles[3], rock_loc, Quaternion.identity) as GameObject;
        }else if(attackPick == 4){ //Shotgun
            StartCoroutine(BurstFire());
        }else if(attackPick == 5){ //Homing
            Instantiate(projectiles[5], wizhardModal.transform.position, Quaternion.identity);
        }else if(attackPick == 6){ //Shield
            StartCoroutine(EnableShield());
        }else if(attackPick == 7){ //Slug
            Vector3 spawnPoint = wizhardModal.transform.position + new Vector3(0, -8f, 0);
        
            GameObject slug = Instantiate(projectiles[6], spawnPoint, Quaternion.identity) as GameObject;
        }

        if(attackCount < 7){
            attackCount += 1;
        }else{
            attackCount = 0;
        }


    }

    void HandAnimator(bool up){
        
        if((attackCount % 2) == 0){
            if(up){
                leftHand.localPosition = new Vector3(-1.4f, 2, 1);
            }else{
                leftHand.localPosition = new Vector3(0, 0.66678f, 1);
            }
        }else{
            if(up){
                rightHand.localPosition = new Vector3(-1.4f, 2, -1);
            }else{
                rightHand.localPosition = new Vector3(0, 0.66678f, -1);
            }
        }

        
        
    }

    /*Wizard Raises hands
    Disappears with particle effects
    Reappears with particle effects */

    IEnumerator StopAndAttack(){

        Debug.Log("Attacking");
        moving = false;

        HandAnimator(true);

        if(wizHealth.GetHealth() < 100){
            SpellCaster();
            SpellCaster();
        }else if(wizHealth.GetHealth() < 50){
            SpellCaster();
            SpellCaster();
            SpellCaster();
        }else{
            SpellCaster();
        }

        yield return new WaitForSeconds(attackDuration);

        HandAnimator(false);

        moving = true;
        cooldownTimer = 0;

    }

    IEnumerator BurstFire(){
        Instantiate(projectiles[1], wizhardModal.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        Instantiate(projectiles[1], wizhardModal.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        Instantiate(projectiles[1], wizhardModal.transform.position, Quaternion.identity);
    }

    IEnumerator EnableShield(){
        wizShield.SetActive(true);

        cooldownTime = cooldownTimer * 3;

        yield return new WaitForSeconds(cooldownTime);

        cooldownTime = cooldownTimer / 3;

        wizShield.SetActive(false);
    }
}
