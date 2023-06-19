using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private static EnemyView _enemy;
    private static Queue<EnemyController> _enemyPool = new Queue<EnemyController>();

    public static void Initialize(EnemyView enemyPrefab)
    {
        _enemy = enemyPrefab;
    }

    public static EnemyController GetEnemy(EnemySO enemySO)
    {
        EnemyController enemy;

        if (_enemyPool.Count > 0)
         {
            enemy = _enemyPool.Dequeue();
            enemy.Setup(enemySO);
        }
        else
        {
            enemy = CreateNewEnemy(enemySO);
        }
        return enemy;
    }

    private static EnemyController CreateNewEnemy(EnemySO enemySO)
    {
        EnemyController enemy = new EnemyController(_enemy, enemySO);
        return enemy;
    }

    public static void ReturnEnemy(EnemyController enemy)
    {
        _enemyPool.Enqueue(enemy);
    }
}
