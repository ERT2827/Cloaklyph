using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject Player;

    [SerializeField] private Vector3 offset = new Vector3(0, 27, -40);
    
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");

        Quaternion camTrans = Quaternion.Euler(45, 0, 0);
        transform.rotation = camTrans; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, Player.transform.position + offset, 0.25f);
    }
}
