using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShooterScript : MonoBehaviour
{
    [Header("Movement parameters")]
    
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Vector3 centrePoint = new Vector3(0, 0, 0); //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    [Header("Shooting parameters")]
    [SerializeField] private GameObject projectilePrefab;

    bool stopped = false; //This is to stop the enemy between cycles
    
    void Start()
    {    
        agent = GetComponent<NavMeshAgent>();
    }

    
    void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !stopped){ //Done with path
            stopped = true;
            
            StartCoroutine(StopNShoot());
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

        Vector3 point;
            if (RandomPoint(centrePoint, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        
        stopped = false;
    }
}
