using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    
    private void Start() {
        SpawnBullets();
    }

    void SpawnBullets(){
        int bulletCount = 7;
        int spread = 2;

        Quaternion newRot = transform.rotation;

        for (int i = 0; i < bulletCount; i++)
        {
            float addedOffset = (i - (bulletCount / 2) * spread) - spread;
            
            newRot = Quaternion.Euler(transform.rotation.x, transform.rotation.y + addedOffset, transform.rotation.z);
            Instantiate(bulletPref, transform.position, newRot);
        }
    }
}
