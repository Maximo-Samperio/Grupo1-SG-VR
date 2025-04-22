using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;   // The bullet prefab to instantiate
    public Transform firePoint;       // Where the bullets spawn from
    public float bulletForce = 20f;   // Speed of the bullet

    public void Shoot()
    {
        // Create the bullet at the firePoint's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Get the Rigidbody component from the bullet to apply force
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Add force to the bullet to make it move
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
        }
    }
}
