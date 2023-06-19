using System.Collections;
using UnityEngine;

public class EnemyService
{
    private EnemyView _enemyView;
    private EnemySO _enemySO;
    private GameService _gameService;
    private int _enemiesSpawnedCount;
    private int _initialDelayInSpawning;
    private int _delayBetweenSpawning;

    public int _enemiesDestroyedCount;

    public EnemyService(EnemySO enemySO, EnemyView enemyView, int initialDelayInSpawning, int delayBetweenSpawning)
    {
        this._enemySO = enemySO;
        this._enemyView = enemyView;
        this._initialDelayInSpawning = initialDelayInSpawning;
        this._delayBetweenSpawning = delayBetweenSpawning;
        _gameService = GameService.Instance;

        StartSpawningEnemies();
    }

    private void StartSpawningEnemies()
    {
       _gameService.StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // wait for x seconds before starting the spawning.
        yield return new WaitForSeconds(_initialDelayInSpawning);

        while (true)
        {
            new EnemyController(_enemyView, _enemySO);

            // For Updating the Gameplay UI
            _enemiesSpawnedCount++;
            _gameService.OnEnemiesSpawned?.Invoke(_enemiesSpawnedCount);

            // Wait for x second before spawning the next enemy
            yield return new WaitForSeconds(_delayBetweenSpawning); 
        }
    }
}