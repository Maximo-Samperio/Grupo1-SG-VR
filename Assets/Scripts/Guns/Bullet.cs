using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static Target;

public class Bullet : BulletsPool
{
    private bool _isActive = false;
    private string targetTag = "Target";
    private string pauseMenuTag = "Pause";

    public void Init(Transform shootingPoint, float bulletSpeed)
    {
        transform.position = shootingPoint.position;
        transform.rotation = shootingPoint.rotation;

        gameObject.SetActive(true);

        _isActive = true;

        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Reset any previous velocity or rotation
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // Add force to the bullet to make it move
            rb.AddForce(shootingPoint.forward * bulletSpeed, ForceMode.Impulse);
        }

        StartCoroutine("StartBulletLifeTime");
    }

    IEnumerator StartBulletLifeTime()
    {
        yield return new WaitForSeconds(2f);

        if (_isActive)
        {
            Destroy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(targetTag))
        {
            Target TargetHit = other.GetComponent<Target>();

            TargetHit.ManageRespawnRoutine();

            ScoreManager.Instance.AddScore((int)TargetHit.targetType);

            Destroy();
        }

        else if(other.CompareTag(pauseMenuTag))
        {
            other.GetComponent<PauseMenuController>().EnterPauseMenu();

            Destroy();
        }
    }

    public void Destroy()
    {
        _isActive = false;
        _pool.Release(this);
    }

}
