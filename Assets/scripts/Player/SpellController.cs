using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    delegate void SpellMethod();
    
    
    [Header("Core values")]
    [SerializeField] private GameObject SSP; //Spell Spawn Point, shortened for convenience
    [SerializeField] private float coolDown;
    [SerializeField] private float coolDownTimer;
    bool onCoolDown = false;

    [Header("Input System")]
    [SerializeField] private string inputHistory = "";
    [SerializeField] private List<string> spellInputs;

    [Header("Spell List")]
    [SerializeField] private List<SpellMethod> spellsList = new List<SpellMethod>();
    
    [Header("Prefabs")]
    public GameObject[] spellPrefabs;
    
    [Header("Nearest Detection")]
    [SerializeField] private GameObject[] targets;
    public GameObject nearestTarget;

    private void Awake() {
        targets = GameObject.FindGameObjectsWithTag("Targetable");
    
        //Add spells to the list
        spellsList.Add(Quickshot);
        spellsList.Add(Homingshot);
        spellsList.Add(Big_Rock);
        spellsList.Add(Explosioooon);
        spellsList.Add(AirDisk);
        spellsList.Add(AirDiskDeluxe);
    }

    private void Update() {
        if (coolDownTimer > 0){
            coolDownTimer -= Time.deltaTime;
            onCoolDown = true;
        }else{
            onCoolDown = false;
        }



        if(Input.GetButton("Fire1")){
            int CSN = CheckCurrentSpell(); //CSN Stands for current spell number
            if(!onCoolDown && targets.Length > 0){
                if (CSN != -1)
                {
                    spellsList[CSN]();
                }
                
                inputHistory = "";
                coolDownTimer = coolDown;
            }
        }

        // Arrow Checks
        if (Input.GetKeyDown(KeyCode.LeftArrow)){
            inputHistory += "Left";
        }else if (Input.GetKeyDown(KeyCode.RightArrow)){
            inputHistory += "Right";
        }else if (Input.GetKeyDown(KeyCode.UpArrow)){
            inputHistory += "Up";
        }else if (Input.GetKeyDown(KeyCode.DownArrow)){
            inputHistory += "Down";
        }
    }

    private void FixedUpdate() {
        nearestTarget = nearestFinder();
    }


    GameObject nearestFinder(){
        GameObject tempclosest = null;
        float closestDistance = Mathf.Infinity;

        foreach(GameObject i in targets){
            if(i != null){
                float d = Vector3.Distance(i.transform.position, transform.position);
                if(d < closestDistance){
                    tempclosest = i;
                    closestDistance = d;
                }
            }
        }

        return tempclosest;
    }

    int CheckCurrentSpell(){
        for (int i = 0; i < spellInputs.Count; i++)
        {
            if(spellInputs[i] == inputHistory){
                Debug.Log(i+1);
                return i + 1;
            }
        }

        return 0;
    }

    // All spells must return void and have no variables

    void Quickshot(){
        GameObject bolt = Instantiate(spellPrefabs[0], SSP.transform.position, Quaternion.identity) as GameObject; 
        bolt.GetComponent<BasicBolt>().Shoot(nearestTarget.transform);
    }

    void Homingshot(){
        GameObject bolt = Instantiate(spellPrefabs[1], SSP.transform.position, Quaternion.identity) as GameObject;
        bolt.GetComponent<HomingBolt>().Shoot(nearestTarget.transform);
    }

    void Big_Rock(){
        Vector3 rock_loc = new Vector3(nearestTarget.transform.position.x, nearestTarget.transform.position.y + 5, nearestTarget.transform.position.z);
        
        GameObject rock = Instantiate(spellPrefabs[2], rock_loc, Quaternion.identity) as GameObject;
    }

    void Explosioooon(){
        GameObject explosion = Instantiate(spellPrefabs[3], SSP.transform.position, Quaternion.identity) as GameObject;
    }
    
    void AirDisk(){
        GameObject airdisk = Instantiate(spellPrefabs[4], SSP.transform.position, Quaternion.identity) as GameObject;
    }

    void AirDiskDeluxe(){
        GameObject airdiskdeluxe = Instantiate(spellPrefabs[5], SSP.transform.position, Quaternion.identity) as GameObject;
    }
}
