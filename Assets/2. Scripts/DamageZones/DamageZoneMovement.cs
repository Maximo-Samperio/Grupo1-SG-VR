using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class DamageZoneMovement : MonoBehaviour
{
    [SerializeField] private float _zoneSpeed;
    [SerializeField] private float _zoneDamage;
    [SerializeField] private float _zoneTimeBetweenHit;

    private float _hitTimer;
    private bool _hitPlayer;
    
    // private PlayerLifeController playerRef;

    // Start is called before the first frame update
    void Start()
    {
        _hitTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveFordward();
    }

    private void CheckIfHit()
    {
        _hitTimer += Time.deltaTime;

        if (_hitPlayer && _hitTimer > _zoneTimeBetweenHit)
        {
            _hitTimer = 0;
            
            // playerRef.TakeDamage();
        }
    }

    private void MoveFordward()
    {
        transform.Translate(Vector3.forward * _zoneSpeed * Time.deltaTime * -1f); 
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _hitPlayer = true;

            //playerRef = other.GetComponent<PlayerLifeController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _hitPlayer = false;
        }
    }
}
