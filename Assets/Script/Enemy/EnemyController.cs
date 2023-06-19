
using System.Collections;
using UnityEngine;

public class EnemyController
{
    private EnemyView _enemyView;
    private EnemySO _enemySO;
    private Vector3 _target;

    private int _initialDelayInSpawning;
    private int _delayBetweenSpawning;

    private int _enemiesSpawnedCount;

    public EnemyController(EnemySO enemySO, int initialDelayInSpawning, int delayBetweenSpawning)
    {
        _enemySO = enemySO;

        _initialDelayInSpawning = initialDelayInSpawning;
        _delayBetweenSpawning = delayBetweenSpawning;

        GameService.Instance.StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(_initialDelayInSpawning);

        while (true)
        {
            _enemyView = EnemyPool.GetEnemy();

            // Setup enemy properties
            Setup();

            _enemyView.MoveEnemy += MoveTowardsTarget;
            _enemyView.OnDead += OnEnemyDead;

            // For Updating the Gameplay UI
            _enemiesSpawnedCount++;
            GameService.Instance.OnEnemiesSpawned?.Invoke(_enemiesSpawnedCount);

            // Wait for a delay before spawning the next enemy
            yield return new WaitForSeconds(_delayBetweenSpawning);
        }
    }

    public void Setup()
    {
        _enemyView.SetHealth(_enemySO.Health);
        _enemyView.transform.position = GetRandomEdgeSpawnPosition();
        _enemyView.gameObject.SetActive(true);
        _enemyView.GetComponent<BoxCollider>().enabled = true;
    }

    private Vector3 GetRandomEdgeSpawnPosition()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        float spawnDistance = 1.5f;
        float spawnX, spawnY;

        if (Random.value < 0.5f)
        {
            spawnX = Random.Range(0f, 1f) * screenRatio;
            spawnY = Random.value < 0.5f ? 0f : 1f;
        }
        else
        {
            spawnX = Random.value < 0.5f ? 0f : 1f;
            spawnY = Random.Range(0f, 1f);
        }

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0f);
        spawnPosition = Camera.main.ViewportToWorldPoint(spawnPosition);
        spawnPosition.z = 0f;

        Vector3 direction = spawnPosition;
        spawnPosition += direction.normalized * spawnDistance;

        return spawnPosition;
    }

    private void MoveTowardsTarget(Rigidbody rgbd, EnemyView enemyView)
    {
        Vector3 direction = _target - enemyView.transform.position;
        direction.Normalize();

        // Calculate the rotation angle to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smoothly rotate the enemy towards the target direction
        rgbd.MoveRotation(Quaternion.RotateTowards(enemyView.transform.rotation, targetRotation, 180f));

        // Move the enemy in the direction of the target
        rgbd.velocity = direction * 3f;
    }

    private void OnEnemyDead()
    {
        _enemyView.MoveEnemy -= MoveTowardsTarget;
        _enemyView.OnDead -= OnEnemyDead;
    }
}