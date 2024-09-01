using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum TimerMode { COMBAT, TIMETRIAL }

public class Timer : MonoBehaviour
{
    public float totalTime;
    public TMP_Text clockText;
    public bool timeRunning = true;
    public TimerMode TM;
    public GameObject QRCode;

    private void FixedUpdate() {
        if(TM == TimerMode.COMBAT && timeRunning){
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Targetable");
            if(enemies.Length <= 0){
                endTimer();
            }
        }
        
        if(timeRunning){
            totalTime += Time.deltaTime;

            TimeSpan TI = TimeSpan.FromSeconds(totalTime);

            clockText.SetText(TI.ToString());
        }
    }

    public void endTimer(){
        timeRunning = false;
        clockText.color = Color.green;

        if(QRCode != null){
            QRCode.SetActive(true);
        }

    }
}
