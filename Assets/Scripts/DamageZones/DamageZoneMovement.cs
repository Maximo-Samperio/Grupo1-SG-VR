using UnityEngine;
using UnityEngine.Pool;

public class DamageZoneMovement : MonoBehaviour
{
    [SerializeField] private float _zoneSpeed;
    [SerializeField] private float _zoneDamage;
    [SerializeField] private float _zoneTimeBetweenHit;
    [SerializeField] private bool _amIBig;

    private float _hitTimer;
    private bool _hitPlayer;
    private ObjectPool<DamageZoneMovement> _pool; 
    
    private CharacterLifeController playerRef;
    private CharacterFilter playerFeed;

    // Start is called before the first frame update
    void Start()
    {
        _hitTimer = _zoneTimeBetweenHit;
    }

    // Update is called once per frame
    void Update()
    {
        MoveFordward();
        CheckIfHit();
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

            playerRef.UpdateLife(_zoneDamage);
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

        if (other.CompareTag("Player"))
        {
            playerRef = other.gameObject.GetComponent<CharacterLifeController>();
            playerFeed = other.gameObject.GetComponent<CharacterFilter>();

            if (playerFeed != null)
                playerFeed.ActivateFilter(_amIBig);

            _hitPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            

            _hitPlayer = false;

            if (playerFeed == null)
            {
                playerFeed = other.gameObject.GetComponent<CharacterFilter>();
            }
            else
            {
                playerFeed.DeactivateFilter();
            }
        }
    }

    private void Destroy() => _pool.Release(this);
}
