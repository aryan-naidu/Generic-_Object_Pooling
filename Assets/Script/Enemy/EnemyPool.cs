public class EnemyPool
{
    private static ObjectPool<EnemyView> _enemyPool;

    public static void Initialize(EnemyView enemyPrefab)
    {
        _enemyPool = new ObjectPool<EnemyView>(enemyPrefab);
    }

    public static EnemyView GetEnemy()
    {
        return _enemyPool.GetObject();
    }

    public static void ReturnEnemy(EnemyView enemy)
    {
        _enemyPool.ReturnObject(enemy);
    }
}
