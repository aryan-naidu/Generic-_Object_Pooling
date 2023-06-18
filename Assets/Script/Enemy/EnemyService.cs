using System.Collections;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private EnemyView _enemyView;
    [SerializeField] private int _initialDelayInSpawning = 2;
    [SerializeField] private int _delayBetweenSpawning = 2;

    private int _enemiesSpawnedCount;

    public int _enemiesDestroyedCount;

    private void Start()
    {
        EnemyPool.Initialize(_enemyView);
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(_initialDelayInSpawning);

        while (true)
        {
            EnemyController enemyController = EnemyPool.GetEnemy(_enemySO);

            // For Updating the Gameplay UI
            _enemiesSpawnedCount++;
            GameService.instance.OnEnemiesSpawned?.Invoke(_enemiesSpawnedCount);

            // Wait for 1 second before spawning the next enemy
            yield return new WaitForSeconds(_delayBetweenSpawning);
        }
    }
}
