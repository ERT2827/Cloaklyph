using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreen : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject screenBanner;
    [SerializeField] private List<GameObject> pents;

    PlayerHealth playerHealth;
    PlayerController playerController;
    bool loading = true;
    float changeTime = 0.2f;
    [SerializeField] private float loadTime;
    int currentPent;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        playerHealth = player.GetComponent<PlayerHealth>();
        playerController = player.GetComponent<PlayerController>(); 

        foreach(Transform child in screenBanner.transform){
            pents.Add(child.gameObject);
        }

        loadTime = Random.Range(1.5f, 2.3f);

        StartCoroutine(PentChanger());
        StartCoroutine(FinishLoad());
    }

    // Update is called once per frame
    void Update()
    {
        if(loading){
            playerHealth.invincible = true;
            playerController.controlsDisabled = true;
        }
    }

    IEnumerator PentChanger(){
        pents[0].SetActive(true);
        pents[1].SetActive(false);
        pents[2].SetActive(false);

        yield return new WaitForSeconds(changeTime);

        pents[0].SetActive(false);
        pents[1].SetActive(true);
        pents[2].SetActive(false);

        yield return new WaitForSeconds(changeTime);

        Debug.Log("point reached");
        
        pents[0].SetActive(false);
        pents[1].SetActive(false);
        pents[2].SetActive(true);

        yield return new WaitForSeconds(changeTime);

        StartCoroutine(PentChanger());
    }

    IEnumerator FinishLoad(){
        yield return new WaitForSeconds(loadTime);
        
        loading = false;
        screenBanner.SetActive(false);
        playerHealth.invincible = false;
        playerController.controlsDisabled = false;
    }
}
