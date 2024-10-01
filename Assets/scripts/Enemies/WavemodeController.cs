using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WavemodeController : MonoBehaviour
{
    [SerializeField] private TMP_Text UI_Text;
    PlayerHealth PH;
    
    [Header("Wave Control")]
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private GameObject spawner;
    [SerializeField] private float breakTime;
    private int wave;
    
    [Header("Timer")]
    private float waveTime;
    private float totalTime;

    public bool onBreak;

    private void Awake() {
        if(UI_Text == null){
            UI_Text = GameObject.Find("info_text").GetComponent<TMP_Text>();
        }

        PH = GameObject.Find("Player").GetComponent<PlayerHealth>();

        wave = 0;
        StartCoroutine(StartWave());
    }

    // Update is called once per frame
    void Update()
    {   
        if(!onBreak){
            waveTime += Time.deltaTime;
            totalTime += Time.deltaTime;

            TimeSpan WT = TimeSpan.FromSeconds(waveTime);
            TimeSpan TT = TimeSpan.FromSeconds(totalTime);


            UI_Text.SetText("Wave: " + wave.ToString() + "\n" + WT.ToString("\\:mm\\:ss") + "/" + TT.ToString("\\mm\\:ss"));
        }
    }

    public void WaveStarter(){ //Named as such because I needed to abstract the coroutine
        if(!onBreak){
            StartCoroutine(StartWave());
        }

        onBreak = true;
    }

    IEnumerator StartWave(){
        // if(spawner != null){
        //     spawner.SetActive(false);
        //     Destroy(spawner);
        // }
        

        wave += 1;

        UI_Text.color = Color.green;

        PH.invincible = true;    

        yield return new WaitForSeconds(breakTime);

        waveTime = 0;
        UI_Text.color = Color.white;    

        if(wave <= 5){
            GameObject spawner = Instantiate(spawners[wave - 1], transform.position, Quaternion.identity) as GameObject;

            spawner.transform.parent = transform;
        }else if(wave % 10 == 0){
            spawner = Instantiate(spawners[10], transform.position, Quaternion.identity) as GameObject;

            spawner.transform.parent = transform;
        }else{
            int wavechoice = UnityEngine.Random.Range(5, 9);
            
            spawner = Instantiate(spawners[wavechoice], transform.position, Quaternion.identity) as GameObject;

            spawner.transform.parent = transform;
        }

        onBreak = false;

        yield return new WaitForSeconds(1f);

        PH.invincible = false; 
    }


}
