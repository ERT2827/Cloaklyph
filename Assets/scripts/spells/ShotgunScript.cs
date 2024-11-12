using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour
{
    [SerializeField] private GameObject bulletPref;
    [SerializeField] private int bulletCount = 7;
    [SerializeField] private int spreadAngle = 30;
    [SerializeField] private int projectileSpeed = 100;
    
    private void Start() {
        SpawnBullets();
    }

    void SpawnBullets(){
        

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPref, transform.position, Quaternion.identity);

            // Calculate the spread angle for this bullet
            float angle = -(spreadAngle / 2) + (i * (spreadAngle / (bulletCount - 1)));

            // Debug.Log(angle);
            
            // Convert the angle to a direction (in world space)
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            Debug.Log("Direction is" + direction);
            
            // Add velocity to the bullet
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = direction * projectileSpeed;
            // Debug.Log(rb.velocity);
        }

        Destroy(gameObject);
    }
}
