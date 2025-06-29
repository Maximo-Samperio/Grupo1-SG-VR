using UnityEngine;
using UnityEngine.Pool;

public class BulletsPool: MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;

    protected ObjectPool<Bullet> _pool;
    public float _bulletSpeed;
    public Transform _spawnPoint;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(CreateBullet, null, OnPutBackInPool, defaultCapacity: 10);
    }

    private void OnPutBackInPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private Bullet CreateBullet()
    {
        var zone = Instantiate(_bulletPrefab);

        return zone;
    }

    public void SpawnBullet()
    {
        var bullet = _pool.Get();

        bullet.Init(_spawnPoint, _bulletSpeed);
    }
}
