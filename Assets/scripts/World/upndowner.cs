using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upndowner : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float changeTime = 1;
    bool dir;
    Vector3 target;

    private void Start() {
        StartCoroutine(directionChanger());
    }

    private void Update() {
        transform.position = Vector3.Lerp(transform.position, target, speed);
    }

    IEnumerator directionChanger(){
        dir = !dir;

        if(dir){
            target = transform.position + new Vector3(0, 3, 0);
        }else{
            target = transform.position - new Vector3(0, 3, 0);
        }

        yield return new WaitForSeconds(changeTime);

        StartCoroutine(directionChanger());
    }
}
