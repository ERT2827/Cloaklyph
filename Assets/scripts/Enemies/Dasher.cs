using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dasher : MonoBehaviour
{
    [Header("Movement parameters")]
    
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Vector3 centrePoint; //Set to world 0 if you want him
    // to be stupid

    Vector3 targetPoint;

    [Header("Shooting parameters")]
    [SerializeField] private GameObject projectilePrefab;

    // AI_Mode curMode; //This Enum controls the mode of the AI
    [SerializeField] private float cooldown;
    float cooldownTimer;


    void Start()
    {    
        agent = GetComponent<NavMeshAgent>();
        centrePoint = transform.position;
        
        // curMode = AI_Mode.Move;
        FindPoint();
    }

    
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        float pointDistance = Vector3.Distance(targetPoint, transform.position);
        
        if(cooldownTimer > cooldown){
            cooldownTimer = 0;
            Shoot();
        }

        if(pointDistance < 1f){
            FindPoint();
        }

    }

    void FindPoint()
    {
        Vector3 randomPoint = centrePoint + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            agent.SetDestination(hit.position);
        }else{
            FindPoint();
        }
    }

    void Shoot(){
        GameObject bolt = Instantiate(projectilePrefab, transform.GetChild(1).position, Quaternion.identity) as GameObject;
    }
}
