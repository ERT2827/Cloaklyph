using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    Vector3 defaultPosition;
    Vector3 downPosition;
    
    float smoothFactor = 3;


    private void Awake() {
        defaultPosition = transform.position;
        downPosition = transform.position + new Vector3(0, -15f, 0);

        // UniversalVariables.playerState = PlayerState.Combat;
    }
    
    private void Update() {
        // Debug.Log("Playerstate is: " + UniversalVariables.playerState);
        
        if(UniversalVariables.playerState == PlayerState.Exploring){
            transform.position = Vector3.Lerp(transform.position, downPosition, Time.deltaTime * smoothFactor);
        }else
        {
            transform.position = Vector3.Lerp(transform.position, defaultPosition, Time.deltaTime * smoothFactor);
        }
    }
}
