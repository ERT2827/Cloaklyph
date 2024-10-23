using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountainFloor : MonoBehaviour
{
    public Transform fogTrans;
    public Transform floorTrans;

    Vector3 fogStart;
    Vector3 floorStart;

    Transform player;

    public float fallSpeed = 0.1f;
    
    // Start is called before the first frame update
    void Start()
    {
        fogTrans = transform.GetChild(0);
        floorTrans = transform.GetChild(1);

        fogStart = fogTrans.position;  
        floorStart = floorTrans.position;

        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float startDistance = Vector3.Distance(player.position, Vector3.zero);
        
        fogTrans.position = new Vector3(fogTrans.position.x, fogStart.y - (startDistance * fallSpeed), fogTrans.position.z);
        floorTrans.position = new Vector3(floorTrans.position.x, floorStart.y - (startDistance * fallSpeed), floorTrans.position.z);
    }
}
