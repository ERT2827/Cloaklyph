using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AI_Mode { Move, Act }

public class ShooterScript : MonoBehaviour
{
    [Header("Movement parameters")]
    
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Vector3 centrePoint; //Set to world 0 if you want him
    // to be stupid

    [Header("Shooting parameters")]
    [SerializeField] private GameObject projectilePrefab;

    AI_Mode curMode; //This Enum controls the mode of the AI
    [SerializeField] private float cooldown;
    float cooldownTimer;


    void Start()
    {    
        agent = GetComponent<NavMeshAgent>();
        centrePoint = transform.position;
        
        curMode = AI_Mode.Move;
    }

    
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        
        if (agent.remainingDistance <= agent.stoppingDistance && curMode == AI_Mode.Move && cooldown < cooldownTimer){ //Done with path            
            curMode = AI_Mode.Act;
            
            StartCoroutine(StopNShoot());
        }else if(curMode == AI_Mode.Move && cooldown*2 < cooldownTimer){
            FindNewPos();

            cooldownTimer = 0;
        }

    }

    void FindNewPos(){
        Vector3 point;
        if (RandomPoint(centrePoint, range, out point)) //pass in our centre point and radius of area
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
            agent.SetDestination(point);
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

    IEnumerator StopNShoot(){
        GameObject bolt = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        
        yield return new WaitForSeconds(1f);

        FindNewPos();
        
        cooldownTimer = 0;
        curMode = AI_Mode.Move;
    }
}
