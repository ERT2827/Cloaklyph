using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fizzle_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AutoDestruct(){
        yield return new WaitForSeconds(2f);

        Destroy(gameObject);
    }
}
