using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fogScript : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private void Start() {
        if(player == null){
            player = GameObject.FindWithTag("Player");
        }
    }

    private void Update() {
        Transform playerPos = player.transform;

        transform.position = new Vector3(playerPos.position.x, 0, playerPos.position.z);
    }
}
