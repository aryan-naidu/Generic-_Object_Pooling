using UnityEngine;

public class BulletPool : GenericObjectPool<BulletView>
{
    private static BulletPool _instance;

    // It takes a BulletView prefab as a parameter and passes it to the base class constructor (base(prefab)).
    private BulletPool(BulletView prefab) : base(prefab) { }

    public static void Initialize(BulletView bulletPrefab)
    {
        if (_instance != null)
        {
            Debug.LogWarning("BulletPool is already initialized!");
            return;
        }

        _instance = new BulletPool(bulletPrefab);
    }

    public static BulletView GetBullet()
    {
        return _instance.GetObject();
    }

    public static void ReturnBullet(BulletView bullet)
    {
        _instance.ReturnObject(bullet);
    }
}
