using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationDelay : MonoBehaviour
{
    [SerializeField] private List<MonoBehaviour> scripts = new List<MonoBehaviour>();
    [SerializeField] private float delayTime = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Activator());
    }

    IEnumerator Activator(){
        yield return new WaitForSeconds(delayTime);
    }
}
