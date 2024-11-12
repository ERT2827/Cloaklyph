using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HammerTime : MonoBehaviour
{
    [Header("Movement parameters")]
    
    public NavMeshAgent agent;

    [SerializeField] private float aggroThreshold = 5;
    Vector3 targetPoint;

    private AI_Mode curMode; //This Enum controls the mode of the AI
    
    
    [Header("Attacking parameters")]
    GameObject player;
    public GameObject shield;
    Transform hammer;
    [SerializeField] private float cooldown;
    [SerializeField] private float swingAnimSpeed;
    [SerializeField] private GameObject bulletPref;
    float cooldownTimer;

    Quaternion hammerTarget;



    [Header("Look parameters")]
    [SerializeField] private float lookspeed = 1;


    void Start()
    {    
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player");
        hammer = transform.GetChild(0);
    }

    
    void Update()
    {
        float playerDist = Vector3.Distance(player.transform.position, transform.position);
    
        cooldownTimer += Time.deltaTime;

        if(cooldownTimer > cooldown && curMode == AI_Mode.Move){
            StartCoroutine(Attack());
        }else if(playerDist > aggroThreshold && curMode == AI_Mode.Move){
            StartCoroutine(SetPosition());
        }

        if(hammer.rotation != hammerTarget){
            hammer.rotation = Quaternion.Slerp(hammer.rotation, hammerTarget, Time.deltaTime * swingAnimSpeed);
        }

        

        

        //Face Direction
        Quaternion lookTarget = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookTarget, Time.deltaTime * lookspeed);

    }

    IEnumerator SetPosition(){
        targetPoint = player.transform.position;


        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            agent.SetDestination(targetPoint);
            curMode = AI_Mode.Move;
        }else{
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(SetPosition());
        }
    
    }

    
    //This was the old way of me spawning bullets
    // void SpawnBullets(){
    //     int bulletCount = 7;
    //     int spread = 2;

    //     Quaternion newRot = transform.rotation;

    //     for (int i = 0; i < bulletCount; i++)
    //     {
    //         float addedOffset = (i - (bulletCount / 2) * spread) - spread;
            
    //         newRot = Quaternion.Euler(transform.rotation.x, transform.rotation.y + addedOffset, transform.rotation.z);
    //         Instantiate(bulletPref, transform.position, newRot);
    //     }
    // }

    IEnumerator Attack(){
        shield.SetActive(false);
        hammerTarget = Quaternion.Euler(12, 0, 90);

        Vector3 spawnPoint = transform.position + transform.forward * 4 + transform.up * 2;

        GameObject shotGun = Instantiate(bulletPref, spawnPoint, transform.rotation) as GameObject;

        curMode = AI_Mode.Act;

        yield return new WaitForSeconds(1.2f);

        curMode = AI_Mode.Move;
        
        shield.SetActive(true);
        hammerTarget = Quaternion.Euler(-90, 0, 90);

        StartCoroutine(SetPosition());
        cooldownTimer = 0;
    }
}
