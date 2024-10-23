using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SwordsmanScritp : MonoBehaviour
{
    [Header("Movement parameters")]
    
    public NavMeshAgent agent;
    Vector3 targetPoint;

    [Header("Attack parameters")]

    [SerializeField] private GameObject attackPrefab;

    GameObject player;

    private AI_Mode curMode; //This Enum controls the mode of the AI
    [SerializeField] private float cooldown;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();

        setPosition();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(targetPoint);

        float pointDistance = Vector3.Distance(targetPoint, transform.position);
        
        if(pointDistance > 0.4f && curMode == AI_Mode.Move){
            StartCoroutine(AttackCooldown());
        }
    }

    void setPosition(){
        targetPoint = player.transform.position;


        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        { 
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            agent.SetDestination(targetPoint);
        }else{
            StartCoroutine(AttackCooldown());
        }
    
    }

    IEnumerator AttackCooldown(){
        if(curMode == AI_Mode.Move){
            Instantiate(attackPrefab, transform.position, Quaternion.identity);
        }

        curMode = AI_Mode.Act;

        yield return new WaitForSeconds(cooldown);

        setPosition();

        curMode = AI_Mode.Move;
    }
}
