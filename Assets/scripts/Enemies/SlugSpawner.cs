using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlugSpawner : MonoBehaviour
{
    [Header("Movement parameters")]
    public NavMeshAgent agent;
    public float range; //radius of sphere
    public Vector3 centrePoint; //Set to world 0 if you want him
    // to be stupid
    
    GameObject player;
    float playerDist = 0;
    float oldDist = 0;
    [SerializeField] private float cowardThreshold = 5;

    [Header("Spawning parameters")]
    [SerializeField] private GameObject slugPrefab;
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private AI_Mode spawner_mode;

    [SerializeField] private float cooldown;
    float cooldownTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");

        centrePoint = transform.position;

        spawnPosition = transform.GetChild(5).transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerDist = Vector3.Distance(player.transform.position, transform.position);
    
        cooldownTimer += Time.deltaTime;

        transform.LookAt(player.transform);

        if(playerDist < cowardThreshold){
            FindNewPos();
        }else if (spawner_mode == AI_Mode.Move && cooldown < cooldownTimer){ //Done with path            
            spawner_mode = AI_Mode.Act;
            
            StartCoroutine(SpawnSlug());
        }else if(spawner_mode == AI_Mode.Move && cooldown*2 < cooldownTimer){
            FindNewPos();

            cooldownTimer = 0;
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

    IEnumerator SpawnSlug(){
        GameObject slug = Instantiate(slugPrefab, spawnPosition.position, Quaternion.identity) as GameObject;
        
        // transform.GetChild(2).transform.position.y += 1;
        // transform.GetChild(4).transform.position.y += 1;
        
        yield return new WaitForSeconds(1.5f);

        // transform.GetChild(2).transform.position.y -= 1;
        // transform.GetChild(4).transform.position.y -= 1;

        FindNewPos();
        
        cooldownTimer = 0;
        spawner_mode = AI_Mode.Move;
    }
}
