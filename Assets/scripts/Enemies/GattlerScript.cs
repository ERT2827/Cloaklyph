using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GattlerScript : MonoBehaviour
{
    [Header("Movement parameters")]
    
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Vector3 centrePoint; //Set to world 0 if you want him
    // to be stupid

    float playerDist = 0;
    float oldDist = 0;
    [SerializeField] private float cowardThreshold = 5;

    [Header("Shooting parameters")]
    [SerializeField] private GameObject projectilePrefab;
    GameObject player;
    AI_Mode curMode; //This Enum controls the mode of the AI
    [SerializeField] private float cooldown;
    float cooldownTimer;

    [Header("Gun Animation")]
    Transform gunTrans;
    [SerializeField] private float spinSpeed;


    void Start()
    {    
        agent = GetComponent<NavMeshAgent>();
        centrePoint = transform.position;

        player = GameObject.Find("Player");
        
        curMode = AI_Mode.Move;

        gunTrans = transform.GetChild(0);
    }

    
    void Update()
    {
        playerDist = Vector3.Distance(player.transform.position, transform.position);
    
        cooldownTimer += Time.deltaTime;

        transform.LookAt(player.transform);

        if(playerDist < cowardThreshold){
            curMode = AI_Mode.Move;
            FindNewPos();
        }else{
            curMode = AI_Mode.Act;
        }

        if(curMode == AI_Mode.Act){
            gunTrans.Rotate(spinSpeed, 0, 0);

            cooldownTimer += Time.deltaTime;
            if(cooldownTimer > cooldown){
                GameObject bolt = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
                cooldownTimer = 0;
            }
        }
        
        

    }

    void FindNewPos(){
        Vector3 point;
        if (RandomPoint(centrePoint, range, out point)) //pass in our centre point and radius of area
        {
            float pointDistance = Vector3.Distance(point, player.transform.position);
            
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            
            if(pointDistance > playerDist && pointDistance > oldDist){
                agent.SetDestination(point);

                pointDistance = oldDist;
            }else
            {
                FindNewPos();
            } 
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

}
