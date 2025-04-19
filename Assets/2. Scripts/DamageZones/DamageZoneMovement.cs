using UnityEngine;
using UnityEngine.Pool;

public class DamageZoneMovement : MonoBehaviour
{
    [SerializeField] private float _zoneSpeed;
    [SerializeField] private float _zoneDamage;
    [SerializeField] private float _zoneTimeBetweenHit;

    private float _hitTimer;
    private bool _hitPlayer;
    private ObjectPool<DamageZoneMovement> _pool; 
    
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

    public void Init(ObjectPool<DamageZoneMovement> pool, bool firstSpawn)
    {
        if(firstSpawn)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);

        _pool = pool;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamageZoneDestroyer"))
        {
            Destroy();
        }
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

    private void Destroy() => _pool.Release(this);
}
