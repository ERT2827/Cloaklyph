using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class saveManager : MonoBehaviour
{
    [Header("Health Systems")]
    public bool[] healthUpgrades = new bool[6];
    public int[] healthSave = new int[3];

    [Header("Gameplay Upgrades")]
    
    // Counter?

    // 

    [Header("Other Important Features")]

    public string next_Level;
    public Vector3 load_Location;

    [Header("Friends of Save Manager")]
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerHealth playerHealth;


    private void Start() {
        if (player == null){
            player = GameObject.FindWithTag("Player");
        }

        if(player != null){
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        
        loadGame();

        if(player != null){
            player.transform.position = load_Location;
            load_Location = new Vector3(0, 2.3f, 0);

            saveGame();
        }
        
    }

    public void saveGame(){
        SaveData.SavePlayer(this);
    }

    public void loadGame(){
        Playerdata data = SaveData.LoadPlayer();
        
        healthUpgrades[0] = data.healthUpgrades[0];
        healthUpgrades[1] = data.healthUpgrades[1];
        healthUpgrades[2] = data.healthUpgrades[2];
        healthUpgrades[3] = data.healthUpgrades[3];
        healthUpgrades[4] = data.healthUpgrades[4];
        healthUpgrades[5] = data.healthUpgrades[5];

        // Loadlocation and scene functions
        load_Location = new Vector3(data.load_LocationX, data.load_LocationY, data.load_LocationZ); 

        if(player != null){
            //Set player variables
            playerHealth.coreOrange = healthUpgrades[0];
            playerHealth.coreGreen = healthUpgrades[1];
            playerHealth.coreWhite = healthUpgrades[2];

            playerHealth.outerOrange = healthUpgrades[3];
            playerHealth.outerGreen = healthUpgrades[4];
            playerHealth.outerWhite = healthUpgrades[5];

            playerHealth.SetHealth();
        }        
    }

    public void resetprogress(){
        healthUpgrades[0] = false;
        healthUpgrades[1] = false;
        healthUpgrades[2] = false;
        healthUpgrades[3] = false;
        healthUpgrades[4] = false;
        healthUpgrades[5] = false;

        healthSave[0] = 0;
        healthSave[1] = 0;
        healthSave[2] = 0;

        load_Location = new Vector3(0, 0, 0);


        saveGame();
    }

    public void IncreaseHealth(int UN){ //UN = Upgrade Number
        if(!healthUpgrades[UN]){
            healthUpgrades[UN] = true;

            playerHealth.coreOrange = healthUpgrades[0];
            playerHealth.coreGreen = healthUpgrades[1];
            playerHealth.coreWhite = healthUpgrades[2];

            playerHealth.outerOrange = healthUpgrades[3];
            playerHealth.outerGreen = healthUpgrades[4];
            playerHealth.outerWhite = healthUpgrades[5];

            if(UN > 2){
                UN -= 3;
                Debug.Log(UN);
                playerHealth.health[UN] = playerHealth.maxHealth[UN];
            }else{
                playerHealth.health[UN] = playerHealth.maxHealth[UN];
            }

            saveGame();
        }
    }

    public void LoadNewLevel(string LN, Vector3 LL){ //LN = Level Name, LL = Load Location
        next_Level = LN;
        load_Location = LL;

        saveGame();

        SceneManager.LoadScene(next_Level, LoadSceneMode.Single);

    }
}
