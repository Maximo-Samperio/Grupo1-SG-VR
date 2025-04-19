using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class DamageZonePool : MonoBehaviour
{
    [SerializeField] private float _timeBetweenZones;
    [SerializeField] private DamageZoneMovement _BigDamageZone;
    [SerializeField] private DamageZoneMovement _feetDamageZone;
    [SerializeField] private Transform _spawnPoint1;
    [SerializeField] private Transform _spawnPoint2;
    [SerializeField] private Transform _spawnPoint3;

    private ObjectPool<DamageZoneMovement> _pool;
    private float _spawnZoneTimer;

    private void Awake()
    {
        _pool = new ObjectPool<DamageZoneMovement>(CreateZone, null, OnPutBackInPool, defaultCapacity: 6);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _spawnZoneTimer += Time.deltaTime;

        if (_spawnZoneTimer > _timeBetweenZones)
        {
            SpawnDangerZone(false);
            _spawnZoneTimer = 0;
        }
    }


    private void OnPutBackInPool(DamageZoneMovement zone)
    {
        zone.gameObject.SetActive(false);
    }

    private DamageZoneMovement CreateZone()
    {
        var zone = Instantiate(GetRandomPrefabToSpawn());

        return zone;
    }

    private Transform GetRandomSpawnPoint()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                return _spawnPoint1;

            case 1:
                return _spawnPoint2;

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

    private void SpawnDangerZone(bool firstSpawn)
    {
        var dangerZone = _pool.Get();

        dangerZone.transform.position = GetRandomSpawnPoint().position;

        dangerZone.Init(_pool, firstSpawn);
    }
}
