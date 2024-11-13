using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDisable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int r = Random.Range(0, 100);

        if(r > 5){
            gameObject.SetActive(false);
        }
    }
}
