using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private static EnemyView _enemyPrefab;
    private static Queue<EnemyView> _enemyPool = new Queue<EnemyView>();

    public static void Initialize(EnemyView enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
    }

    public static EnemyView GetEnemy()
    {
        EnemyView enemy;

        if (_enemyPool.Count > 0)
        {
            enemy = _enemyPool.Dequeue();
            enemy.gameObject.SetActive(true);
            Debug.Log("resusing");
        }
        else
        {
            enemy = CreateNewEnemy();
        }
        return enemy;
    }

    private static EnemyView CreateNewEnemy()
    {
        Debug.Log("new");
        EnemyView enemy = GameObject.Instantiate(_enemyPrefab);
        return enemy;
    }

    public static void ReturnEnemy(EnemyView enemy)
    {
        enemy.gameObject.SetActive(false);
        _enemyPool.Enqueue(enemy);
    }
}
