using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletView _bulletPrefab;
    private static List<BulletView> _bulletPool = new List<BulletView>();

    public static void Initialize(BulletView bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }

    public static BulletView GetBullet()
    {
        BulletView bullet;

        if (_bulletPool.Count > 0)
        {
            bullet = FindAvailableBullet();
            if (bullet == null)
            {
                bullet = CreateNewBullet();
                _bulletPool.Add(bullet);
            }
        }
        else
        {
            bullet = CreateNewBullet();
            _bulletPool.Add(bullet);
        }

        bullet.gameObject.SetActive(true);
        bullet.IsUsed = true;
        return bullet;
    }

    private static BulletView FindAvailableBullet()
    {
        foreach (BulletView bullet in _bulletPool)
        {
            if (!bullet.IsUsed)
            {
                return bullet;
            }
        }
        return null;
    }

    private static BulletView CreateNewBullet()
    {
        BulletView bullet = GameObject.Instantiate(_bulletPrefab).GetComponent<BulletView>();
        bullet.IsUsed = false;
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    public static void ReturnBullet(BulletView bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.IsUsed = false;
    }
}
