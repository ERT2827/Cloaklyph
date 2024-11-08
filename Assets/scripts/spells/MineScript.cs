using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineScript : MonoBehaviour
{
    [SerializeField] private int damage;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        StartCoroutine(Solidify());
    }

    // Update is called once per frame
    void Update()
    {
        float playerDist = Vector3.Distance(player.transform.position, transform.position);

        if(playerDist > 55){
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other) {
        EnemyHealth EHP = other.gameObject.GetComponent<EnemyHealth>();

        if(other.gameObject.tag == "Targetable" && EHP != null){
            EHP.TakeDamage(damage, 3);
            Destroy(gameObject);    
        }


    }

    IEnumerator Solidify(){
        yield return new WaitForSeconds(0.5f);

        gameObject.GetComponent<Collider>().enabled = true;
    }
}
