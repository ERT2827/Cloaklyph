using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameMode gameMode;
    
    
    private void Start() {
        if (gameMode == GameMode.Endless){
            UniversalVariables.playerState = PlayerState.Combat; 
        }else{
            UniversalVariables.playerState = PlayerState.Exploring;
        }

    }
}
