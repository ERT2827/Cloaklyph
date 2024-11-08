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
    [SerializeField] private PlayerHealth playerHealth;
    bool onCoolDown = false;

    [Header("Input System")]
    [SerializeField] private string inputHistory = "";
    [SerializeField] private List<string> spellInputs;

    [Header("Spell List")]
    [SerializeField] private List<SpellMethod> spellsList = new List<SpellMethod>();
    
    [Header("Prefabs")]
    public GameObject[] spellPrefabs;
    
    [Header("Nearest Detection")]
    [SerializeField] private List<GameObject> targets = new List<GameObject>(); 
    public GameObject nearestTarget;
    public bool inGarden;

    [Header("Immortal Effects")]
    [SerializeField] private GameObject[] colorParts; 
    [SerializeField] private Material purple_Mat;

    private void Awake() {
        AddTargets();
        playerHealth = gameObject.GetComponent<PlayerHealth>();
        
        
        //Add spells to the list
        spellsList.Add(Fizzle);
        spellsList.Add(Homingshot);
        spellsList.Add(Big_Rock);
        spellsList.Add(Explosioooon);
        spellsList.Add(AirDisk);
        spellsList.Add(AirDiskDeluxe);
        spellsList.Add(ArrowRain);
        spellsList.Add(Scarab);
        spellsList.Add(Airburst);
        spellsList.Add(SelfHeal);
        spellsList.Add(Melee);
        spellsList.Add(BasicShot);
        spellsList.Add(SniperShot);
        spellsList.Add(DeathGarden);
        spellsList.Add(Immortality);
        spellsList.Add(Telekill);
        spellsList.Add(Laser);
        spellsList.Add(Mine);
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
            if(!onCoolDown && nearestTarget != null){
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

        CheckTargets();

        if(targets.Count <= 0){
            AddTargets();
        }
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
                Debug.Log(spellPrefabs[i+1].name);
                return i + 1;
            }
        }

        return 0;
    }

    void CheckTargets(){
        foreach (var i in targets)
        {
            if(i == null){
                targets.Remove(i);
                return;
            }
        }
    }

    public void AddTargets(){
        GameObject[] temp_Targets = GameObject.FindGameObjectsWithTag("Targetable"); //I was forced to change this because this doesn't allow me to add new game objects
        foreach (GameObject i in temp_Targets){
            if(!targets.Contains(i)){
                targets.Add(i);
            }
        }
    }

    // All spells must return void and have no variables

    void Fizzle(){
        GameObject bolt = Instantiate(spellPrefabs[0], SSP.transform.position, Quaternion.identity) as GameObject; 
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

    void ArrowRain(){
        Vector3 arrowPosition = transform.position;
        arrowPosition.y = arrowPosition.y + 8;

        int randy = Random.Range(0, 360);
        Quaternion arrowRotation = Quaternion.Euler(0, randy, 0);
        
        GameObject arrow = Instantiate(spellPrefabs[6], arrowPosition, arrowRotation) as GameObject;
    }

    void Scarab(){
       GameObject scarab = Instantiate(spellPrefabs[7], SSP.transform.position, Quaternion.identity) as GameObject; 
    }

    void Airburst(){
        GameObject airburst = Instantiate(spellPrefabs[8], SSP.transform.position, Quaternion.identity) as GameObject;
        airburst.transform.rotation = transform.rotation;
    }

    void SelfHeal(){
        playerHealth.HealSpell();
        // Debug.Log("Benis 9000");
    }

    void Melee(){
        Quaternion q = Quaternion.Euler(90, transform.rotation.y, 0);
        
        GameObject melee = Instantiate(spellPrefabs[10], SSP.transform.position, transform.rotation) as GameObject;
        melee.GetComponent<Melee>().playerTrans = gameObject.transform;
    }

    void BasicShot(){
        GameObject bolt = Instantiate(spellPrefabs[11], SSP.transform.position, Quaternion.identity) as GameObject; 
        bolt.GetComponent<BasicBolt>().Shoot(nearestTarget.transform);
    }
    
    void SniperShot(){
        GameObject bolt = Instantiate(spellPrefabs[12], SSP.transform.position, Quaternion.identity) as GameObject; 
    }

    void DeathGarden(){
        Quaternion q = Quaternion.Euler(-90, 0, 0);
        
        GameObject domain = Instantiate(spellPrefabs[13], SSP.transform.position, q) as GameObject; 

        DomainScript ds = domain.GetComponent<DomainScript>();
        ds.SC = gameObject.GetComponent<SpellController>();
        StartCoroutine(ds.SelfDestruct());
    }

    void Immortality(){
        if(inGarden){
            playerHealth.immortal = true;

            foreach (GameObject g in colorParts)
            {
                g.GetComponent<Renderer>().material = purple_Mat;
            }
        }else{
            GameObject fizzle = Instantiate(spellPrefabs[14], SSP.transform.position, Quaternion.identity) as GameObject; 
        }
    }

    void Telekill(){
        Transform tempfarthest = null;
        float farthestDistance = 0; //IDK if that is correct spelling but now I like it because fart-hest hehe

        
        
        foreach(GameObject i in targets){
            if(i != null  && i.activeSelf){
                float d = Vector3.Distance(i.transform.position, transform.position);
                if(d > farthestDistance && d < 100){
                    tempfarthest = i.transform;
                    farthestDistance = d;
                }
            }
        }

        if(farthestDistance < 100f && tempfarthest != null){
            playerHealth.invincible = true;
            StartCoroutine(playerHealth.TeleKillMode());
            transform.position = tempfarthest.position;
            tempfarthest.gameObject.GetComponent<EnemyHealth>().TakeDamage(6, 1);
        }else{
            GameObject fizzle = Instantiate(spellPrefabs[15], SSP.transform.position, Quaternion.identity) as GameObject; 
        }
    }

    void Laser(){
        GameObject laser = Instantiate(spellPrefabs[16], SSP.transform.position, transform.rotation) as GameObject;
        laser.transform.SetParent(gameObject.transform);
    }

    void Mine(){
       Instantiate(spellPrefabs[17], SSP.transform.position, transform.rotation); 
    }
}
