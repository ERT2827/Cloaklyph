using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotator : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float changeTime = 1;
    int rotationDirectionX;
    int rotationDirectionY;
    int rotationDirectionZ;

    private void Start() {
        StartCoroutine(directionChanger());
    }

    private void Update() {
        Quaternion target = Quaternion.Euler(rotationDirectionX, rotationDirectionY, rotationDirectionZ);
        transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * speed);
    }

    IEnumerator directionChanger(){
        rotationDirectionX = Random.Range(0, 360);
        rotationDirectionY = Random.Range(0, 360);
        rotationDirectionZ = Random.Range(0, 360);

        yield return new WaitForSeconds(changeTime);

        StartCoroutine(directionChanger());
    }
}
