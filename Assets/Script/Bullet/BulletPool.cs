using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private static BulletView _bulletPrefab;
    private static BulletSO _bulletSO;
    private static Queue<BulletController> _bulletPool = new Queue<BulletController>();

    public static void Initialize(BulletView bulletPrefab, BulletSO bulletSO)
    {
        _bulletPrefab = bulletPrefab;
        _bulletSO = bulletSO;
    }

    public static BulletController GetBullet(Transform outTransform)
    {
        BulletController bullet;

        if (_bulletPool.Count > 0)
        {
            bullet = _bulletPool.Dequeue();
            bullet.Setup();
            bullet.Launch(outTransform);
        }
        else
        {
            bullet = CreateNewBullet(outTransform);
        }
        return bullet;
    }

    private static BulletController CreateNewBullet(Transform outTransform)
    {
        BulletController bullet = new BulletController(GameObject.Instantiate(_bulletPrefab), _bulletSO);
        bullet.Launch(outTransform);
        return bullet;
    }

    public static void ReturnBullet(BulletController bullet)
    {
        _bulletPool.Enqueue(bullet);
    }
}
