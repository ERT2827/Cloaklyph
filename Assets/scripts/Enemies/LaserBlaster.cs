using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlaster : MonoBehaviour
{
    [SerializeField] private bool firingCycle; //True = firing, false = cooling down
    [SerializeField] private GameObject laserPref;
    [SerializeField] private float upTime;
    [SerializeField] private float downTime;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float lookSpeed;

    GameObject player;
    Rigidbody wingHolder;

    float cooldownTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        wingHolder = transform.GetChild(0).GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Face Direction
        Quaternion lookTarget = Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookTarget, Time.deltaTime * lookSpeed);
        
        cooldownTimer += Time.deltaTime;

        if(!firingCycle && cooldownTimer > downTime){
            firingCycle = true;
            StartCoroutine(FiringCycle());
        }

        if(firingCycle){
            wingHolder.AddTorque(transform.forward * spinSpeed);
        }
    }

    IEnumerator FiringCycle(){
        GameObject laser = Instantiate(laserPref, transform.position, transform.rotation) as GameObject;
        laser.transform.SetParent(gameObject.transform);
        laser.GetComponent<EvilLaserScript>().upTime = upTime;
        
        yield return new WaitForSeconds(upTime);

        firingCycle = false;
    }
}
