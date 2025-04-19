using UnityEngine;
using UnityEngine.Pool;

public class DamageZonePool : MonoBehaviour
{
    [SerializeField] private DamageZoneMovement _BigDamageZone;
    [SerializeField] private DamageZoneMovement _feetDamageZone;
    [SerializeField] private Transform _spawnPoint1;
    [SerializeField] private Transform _spawnPoint2;
    [SerializeField] private Transform _spawnPoint3;

    private ObjectPool<DamageZoneMovement> _pool;


    private void Awake()
    {
        _pool = new ObjectPool<DamageZoneMovement>(CreateZone, null, OnPutBackInPool, defaultCapacity: 500);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnDangerZone();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }


    private void OnPutBackInPool(DamageZoneMovement zone)
    {
        zone.gameObject.SetActive(false);
    }

    private DamageZoneMovement CreateZone()
    {
        var zone = Instantiate(GetRandomPrefabToSpawn());

        Debug.Log("Instantiate");

        return zone;
    }

    private Transform GetRandomSpawnPoint()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                return _spawnPoint1;

            case 1:
                return _spawnPoint1;

            case 2:
                return _spawnPoint3; 
        }

        return null;
    }

    private DamageZoneMovement GetRandomPrefabToSpawn()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                return _feetDamageZone;

            case 1:
                return _BigDamageZone;
        }

        return null;
    }

    private void SpawnDangerZone()
    {
        var dangerZone = _pool.Get();

        dangerZone.transform.position = GetRandomSpawnPoint().position;

        dangerZone.Init(_pool);
    }
}
