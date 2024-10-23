using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]

public class Playerdata {
    
    [Header("Health Systems")]
    
    public bool[] healthUpgrades = new bool[6];
    public int[] health = new int[3];

    [Header("Gameplay Upgrades")]
    
    // Counter?

    // 

    [Header("Other Important Features")]

    public string next_Level;
    public float load_LocationX;
    public float load_LocationY;
    public float load_LocationZ;
    
    public Playerdata(saveManager Player){
        healthUpgrades[0] = Player.healthUpgrades[0];
        healthUpgrades[1] = Player.healthUpgrades[1];
        healthUpgrades[2] = Player.healthUpgrades[2];
        healthUpgrades[3] = Player.healthUpgrades[3];
        healthUpgrades[4] = Player.healthUpgrades[4];
        healthUpgrades[5] = Player.healthUpgrades[5];

        next_Level = Player.next_Level;

        load_LocationX = Player.load_Location.x;
        load_LocationY = Player.load_Location.y;
        load_LocationZ = Player.load_Location.z;

    }

}
