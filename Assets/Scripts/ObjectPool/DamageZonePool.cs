using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DamageZonePool : MonoBehaviour
{
    [SerializeField] private float _timeBetweenZones;
    [SerializeField] private DamageZoneMovement _BigDamageZone;
    [SerializeField] private DamageZoneMovement _feetDamageZone;
    [SerializeField] private List<Transform> _spawnPoints;
    
    private ObjectPool<DamageZoneMovement> _pool;
    private float _spawnZoneTimer;

    private void Awake()
    {
        _pool = new ObjectPool<DamageZoneMovement>(CreateZone, null, OnPutBackInPool, defaultCapacity: 6);
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

    private Transform GetRandomSpawnPoint() => _spawnPoints[Random.Range(0, _spawnPoints.Count)];

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
