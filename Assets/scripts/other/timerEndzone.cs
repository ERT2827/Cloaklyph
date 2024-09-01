using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timerEndzone : MonoBehaviour
{
    public Timer tmr;

    private void OnTriggerEnter(Collider other) {
        tmr.endTimer();
    }
}
