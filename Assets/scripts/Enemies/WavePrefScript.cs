using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePrefScript : MonoBehaviour
{
    WavemodeController wavemodeController;
    
    private void Start() {
        wavemodeController = transform.parent.gameObject.GetComponent<WavemodeController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.childCount == 0){
            Debug.Log(transform.childCount);
            wavemodeController.WaveStarter();
            Destroy(gameObject);
        }
    }
}
