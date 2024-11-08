using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashbang : MonoBehaviour
{
    Image i;

    private float desiredAlpha = 0.0f;
    private float currentAlpha = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        i = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAlpha <= 0){
            gameObject.SetActive(false);
        }

        currentAlpha = Mathf.MoveTowards(currentAlpha, desiredAlpha, 2.0f * Time.deltaTime);

        var tempColor = i.color;
        tempColor.a = currentAlpha;
        i.color = tempColor;
    }
}
