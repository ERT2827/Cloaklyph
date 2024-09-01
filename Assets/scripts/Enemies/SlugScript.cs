using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform playerTrans;
    [SerializeField] private PlayerHealth playerHealth;
    
    [Header("Slug Stats")]

    [SerializeField] private float slugSpeed;
    // [SerializeField] private float slugDamage;

    
    // Start is called before the first frame update
    void Awake(){
        player = GameObject.FindWithTag("Player");
        playerTrans = player.transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerTrans.position, slugSpeed * Time.deltaTime);
    }

    private void FixedUpdate() {
        // var lookPos = playerTrans.position - transform.position;
        // lookPos.y = 0;
        // var rotation = Quaternion.LookRotation(lookPos);
        // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4f);
        // // transform.rotation = Quaternion.Euler(-90, rotation.y, -45);
    }

    private void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player"){
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }
    }
}
