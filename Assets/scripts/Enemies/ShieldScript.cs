using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShieldScript : MonoBehaviour
{
    [Header("Movement parameters")]
    
    public NavMeshAgent agent;

    [SerializeField] private float aggroThreshold = 5;
    Vector3 targetPoint;

    private AI_Mode curMode; //This Enum controls the mode of the AI
    
    [Header("Attacking parameters")]
    GameObject player;
    [SerializeField] private float cooldown;
    float cooldownTimer;

    [Header("Look parameters")]
    [SerializeField] private float lookspeed = 1;


    void Start()
    {    
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player");
        setPosition();
    }

    
    void Update()
    {
        float playerDist = Vector3.Distance(player.transform.position, transform.position);
    
        cooldownTimer += Time.deltaTime;

        if((playerDist > aggroThreshold || cooldownTimer > cooldown) && curMode == AI_Mode.Move){
            curMode = AI_Mode.Act;
            setPosition();
            cooldownTimer = 0;
        }

        

        //Face Direction
        Quaternion lookTarget = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookTarget, Time.deltaTime * lookspeed);

    }

    void setPosition(){
        targetPoint = player.transform.position;


        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            agent.SetDestination(targetPoint);
            curMode = AI_Mode.Move;
        }
        // else{
        //     setPosition();
        // }
    
    }
}
