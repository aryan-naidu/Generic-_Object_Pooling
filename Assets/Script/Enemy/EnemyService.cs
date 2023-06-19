using System.Collections;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private EnemyView _enemyView;
    [SerializeField] private int _initialDelayInSpawning = 2;
    [SerializeField] private int _delayBetweenSpawning = 2;

    public int _enemiesDestroyedCount;

    private void Start()
    {
        EnemyPool.Initialize(_enemyView);
        EnemyController enemyController = new EnemyController(_enemySO, _initialDelayInSpawning, _delayBetweenSpawning);
    }
}
