using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    private static EnemyView _enemyPrefab;
    private static List<EnemyView> _enemyPool = new List<EnemyView>();

    public static void Initialize(EnemyView enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;
    }

    public static EnemyView GetEnemy()
    {
        EnemyView enemy;

        if (_enemyPool.Count > 0)
        {
            enemy = FindAvailableEnemy();
            if (enemy == null)
            {
                enemy = CreateNewEnemy();
                _enemyPool.Add(enemy);
            }
        }
        else
        {
            enemy = CreateNewEnemy();
            _enemyPool.Add(enemy);
        }

        enemy.gameObject.SetActive(true);
        enemy.IsUsed = true;
        return enemy;
    }

    private static EnemyView FindAvailableEnemy()
    {
        foreach (EnemyView enemy in _enemyPool)
        {
            if (!enemy.IsUsed)
            {
                return enemy;
            }
        }
        return null;
    }

    private static EnemyView CreateNewEnemy()
    {
        EnemyView enemy = GameObject.Instantiate(_enemyPrefab).GetComponent<EnemyView>();
        enemy.IsUsed = false;
        enemy.gameObject.SetActive(false);
        return enemy;
    }

    public static void ReturnEnemy(EnemyView enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.IsUsed = false;
    }
}
