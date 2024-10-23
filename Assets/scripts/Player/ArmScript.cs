using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmType
{
    AT_Up,
    AT_Down,
    AT_Left,
    AT_Right
}

public class ArmScript : MonoBehaviour
{
    [SerializeField] private ArmType AT;
    [SerializeField] private float despawnTime = 0.4f;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        if (AT == ArmType.AT_Up){
            transform.Translate(Vector3.up * Time.deltaTime * 0.1f);
        }else if (AT == ArmType.AT_Down){
            transform.Translate(-Vector3.up * Time.deltaTime * 0.1f);
        }else if (AT == ArmType.AT_Left){
            transform.Translate(-Vector3.left * Time.deltaTime * 0.1f);
        }else if (AT == ArmType.AT_Right){
            transform.Translate(Vector3.left * Time.deltaTime * 0.1f);
        }
    }

    IEnumerator SelfDestruct(){
        yield return new WaitForSeconds(despawnTime);

        Destroy(gameObject);
    }
}
