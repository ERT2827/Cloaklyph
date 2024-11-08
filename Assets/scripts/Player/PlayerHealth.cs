using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]

    public int[] health = new int[3];
    public int[] maxHealth = new int[3];
    
    
    [Header("Bools")]
    bool alive = true;
    public bool invincible = false; //Fighting the urge to call this variable "the guy from fortnite"
    public bool immortal = false;

    [Header("Health Upgrades")]
    public bool coreOrange;
    public bool coreGreen;
    public bool coreWhite;

    public bool outerOrange;
    public bool outerGreen;
    public bool outerWhite;

    [Header("Additional Systems")]
    
    [SerializeField] private LayerMask layher; //I hardly know her
    
    // public bool grounded = true;
    [SerializeField] private Vector3 lastGround;
    [SerializeField] private string reloadScene = "Hub";
    int healthComp = -1; //For the health comparison

    [SerializeField] private GameObject[] hitEffects = new GameObject[3];

    [SerializeField] private TrailRenderer trail;

    [Header("UI Elements")]
    [SerializeField] private GameObject coreOrange_IMG;
    [SerializeField] private GameObject coreGreen_IMG;
    [SerializeField] private GameObject coreWhite_IMG;

    [SerializeField] private GameObject outerOrange_IMG;
    [SerializeField] private GameObject outerGreen_IMG;
    [SerializeField] private GameObject outerWhite_IMG;
    
    private void Start() {
        coreOrange_IMG = GameObject.Find("HealthIconOrangeInner");
        outerOrange_IMG = GameObject.Find("HealthIconOrangeOuter");

        coreGreen_IMG = GameObject.Find("HealthIconGreenInner");
        outerGreen_IMG = GameObject.Find("HealthIconGreenOuter");

        coreWhite_IMG = GameObject.Find("HealthIconWhiteInner");
        outerWhite_IMG = GameObject.Find("HealthIconWhiteOuter");
    }
    
    private void Update() {
        RaycastHit hit;

        int totalHealth = health[0] + health[1] + health[2];

        if (!alive)
        {
            SceneManager.LoadScene(reloadScene, LoadSceneMode.Single);
        }

        if(totalHealth != healthComp){
            healthComp = totalHealth;
            UpdateUI();
        }

        if(Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 1, layher)){
            // Debug.Log(hit);
            lastGround = transform.position;
        }

        if(immortal){
            trail.startColor = new Color (0.35f, 0.1294117f, 0.517647f);
            trail.endColor = new Color (0.35f, 0.1294117f, 0.517647f);
        }else if(invincible){
            trail.startColor = Color.green;
            trail.endColor = Color.green;
        }else{
            trail.startColor = Color.white;
            trail.endColor = Color.white;
        }

        

    }

    public void TakeDamage(int element){
        if(invincible || immortal){
            return;
        }

        if(element == 0){
            health[0] -= 1;
            Debug.Log("Orange Hit" + health[0]);
            if(health[0] < 0){
                alive = false;
            }
            Instantiate(hitEffects[0], transform.position, Quaternion.identity);
        }else if(element == 1){
            health[1] -= 1;
            Debug.Log("Green Hit" + health[1]);
            if(health[1] < 0){
                alive = false;
            }
            Instantiate(hitEffects[1], transform.position, Quaternion.identity);
        }else if(element == 2){
            health[2] -= 1;
            Debug.Log("White Hit" + health[2]);
            if(health[2] < 0){
                alive = false;
            }
            Instantiate(hitEffects[2], transform.position, Quaternion.identity);
        }else{
            alive = false;
        }

        StartCoroutine(DamageIFrames());
    }

    public void FallRespawn(){
        health[0] = 0;
        health[1] = 0;
        health[2] = 0;
        
        transform.position = lastGround;
    }

    public void SetHealth(){
        Debug.Log(coreOrange);
        if (coreOrange) maxHealth[0] += 1;
        if (outerOrange) maxHealth[0] += 1;
        
        if (coreGreen) maxHealth[1] += 1;
        if (outerGreen) maxHealth[1] += 1;
        
        if (coreWhite) maxHealth[2] += 1;
        if (outerWhite) maxHealth[2] += 1;
    }

    // private void UpdateHealth(int orange, int green, int white) {
    //     health[0] = orange;
    //     health[1] = green;
    //     health[2] = white;
    // }

    void UpdateUI(){
        if(health[0] > 0){
            if(health[0] == 1){
                coreOrange_IMG.SetActive(true);
                outerOrange_IMG.SetActive(false);
            }else if(health[0] == 2){
                coreOrange_IMG.SetActive(true);
                outerOrange_IMG.SetActive(true);
            }
        }else{
            coreOrange_IMG.SetActive(false);
            outerOrange_IMG.SetActive(false);
        }

        if(health[1] > 0){
            if(health[1] == 1){
                coreGreen_IMG.SetActive(true);
                outerGreen_IMG.SetActive(false);
            }else if(health[1] == 2){
                coreGreen_IMG.SetActive(true);
                outerGreen_IMG.SetActive(true);
            }
        }else{
            coreGreen_IMG.SetActive(false);
            outerGreen_IMG.SetActive(false);
        }

        if(health[2] > 0){
            if(health[2] == 1){
                coreWhite_IMG.SetActive(true);
                outerWhite_IMG.SetActive(false);
            }else if(health[2] == 2){
                coreWhite_IMG.SetActive(true);
                outerWhite_IMG.SetActive(true);
            }
        }else{
            coreWhite_IMG.SetActive(false);
            outerWhite_IMG.SetActive(false);
        }
    }

    public void KillHeal(int EA){
        if(health[EA] < maxHealth[EA]){
            health[EA] += 1;
        }
    }
    
    public void HealSpell(){
        int HC = Random.Range(0, 3); //HC = Heal choice

        if(health[HC] < maxHealth[HC]){
            health[HC] += 1;
        }
    }

    IEnumerator DamageIFrames(){
        invincible = true;

        yield return new WaitForSeconds(1f);

        invincible = false;
    }

    public IEnumerator TeleKillMode(){
        invincible = true;

        yield return new WaitForSeconds(1f);

        invincible = false;

    }
    // public IEnumerator HealSpellDeluxe(){}
}
