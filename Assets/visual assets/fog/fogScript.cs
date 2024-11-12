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
        Vector3 playerPos = player.transform.position;
        playerPos.y = 0;

        transform.position =  Vector3.Slerp(transform.position, playerPos, 0.01f);
    }
}
