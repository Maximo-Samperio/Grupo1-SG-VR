using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    private ObjectPool<Bullet> _pool;
    private bool _isActive = false;

    public void Init(ObjectPool<Bullet> pool, Transform shootingPoint, float bulletSpeed)
    {
        transform.position = shootingPoint.position;
        transform.rotation = shootingPoint.rotation;

        gameObject.SetActive(true);

        _isActive = true;

        _pool = pool;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Add force to the bullet to make it move
            rb.AddForce(shootingPoint.forward * bulletSpeed, ForceMode.Impulse);
        }

        StartCoroutine("StartBulletLifeTime");
    }

    IEnumerator StartBulletLifeTime()
    {
        yield return new WaitForSeconds(2f);

        if(_isActive)
        {
            Destroy();
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        _isActive = false;
        _pool.Release(this);
    }

} 
