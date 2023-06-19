using UnityEngine;

public class EnemyPool : GenericObjectPool<EnemyView>
{
    private static EnemyPool _instance;

    // It takes a EnemyView prefab as a parameter and passes it to the base class constructor (base(prefab)).
    private EnemyPool(EnemyView prefab) : base(prefab) { }

    public static void Initialize(EnemyView enemyPrefab)
    {
        if (_instance != null)
        {
            Debug.LogWarning("EnemyPool is already initialized!");
            return;
        }

        _instance = new EnemyPool(enemyPrefab);
    }

    public static EnemyView GetEnemy()
    {
        return _instance.GetObject();
    }

    public static void ReturnEnemy(EnemyView enemy)
    {
        _instance.ReturnObject(enemy);
    }
}
