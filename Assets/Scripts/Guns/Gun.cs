using System.Collections;
using UnityEngine;

public class Gun : ObjectWithInteraction
{
    [SerializeField] private GameObject bulletPrefab;   // The bullet prefab to instantiate
    [SerializeField] private Transform firePoint;       // Where the bullets spawn from
    [SerializeField] private float bulletForce;   // Speed of the bullet
    
    private BulletsPool _mag;
    private bool canShoot;

    private void Awake()
    {
        _mag = new BulletsPool();
        _mag._bulletSpeed = bulletForce;
        _mag._spawnPoint = firePoint;

        canShoot = true;
    }

    public void Shoot()
    {
        if(canShoot) {
            _mag.SpawnBullet();
            canShoot = false;
            StartCoroutine("StartBulletIntoChamberTime");
        }
        
    }

    IEnumerator StartBulletIntoChamberTime()
    {
        yield return new WaitForSeconds(.4f);
        canShoot = true;
    }
}
