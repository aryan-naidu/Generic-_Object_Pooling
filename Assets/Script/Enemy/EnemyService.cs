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

        EnemyPool.Initialize(_enemyView);
        EnemyController enemyController = new EnemyController(_enemySO, _initialDelayInSpawning, _delayBetweenSpawning);
    }
}
