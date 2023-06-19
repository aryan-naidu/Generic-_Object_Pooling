using System.Collections.Generic;
using UnityEngine;

public class BulletPool
{
    private static BulletView _bulletPrefab;
    private static Queue<BulletView> _bulletPool = new Queue<BulletView>();

    public static void Initialize(BulletView bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }

    public static BulletView GetBullet()
    {
        BulletView bullet;

        if (_bulletPool.Count > 0)
        {
            Debug.Log("reuisng");
            bullet = _bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
        }
        else
        {
            bullet = CreateNewBullet();
        }
        return bullet;
    }

    private static BulletView CreateNewBullet()
    {
        Debug.Log("new");
        BulletView bullet = GameObject.Instantiate(_bulletPrefab);
        return bullet;
    }

    public static void ReturnBullet(BulletView bullet)
    {
        bullet.gameObject.SetActive(false);
        _bulletPool.Enqueue(bullet);
    }
}
