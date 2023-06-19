using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static ObjectPool<BulletView> _bulletPool;

    public static void Initialize(BulletView bulletPrefab)
    {
        _bulletPool = new ObjectPool<BulletView>(bulletPrefab);
    }

    public static BulletView GetBullet()
    {
        return _bulletPool.GetObject();
    }

    public static void ReturnBullet(BulletView bullet)
    {
        _bulletPool.ReturnObject(bullet);
    }
}
