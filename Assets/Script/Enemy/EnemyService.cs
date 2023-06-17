using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyService : MonoBehaviour
{
    [SerializeField] private ScriptableObject _enemySO;
    [SerializeField] private EnemyView _enemyView;
    private EnemyController _enemyController;
    public float initialDelay=2;
    public float spawnInterval=2;
    public float spawnDistance=10;

    private Transform playerTransform;
    private int enemyCount;

    private void Start()
    {
        enemyCount = 1;

        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                Vector2 randomOffset = Random.insideUnitCircle.normalized * 11;
                Vector3 spawnPosition = new Vector3(randomOffset.x, randomOffset.y, 0f);

                Instantiate(_enemyView, spawnPosition, Quaternion.identity);
            }

            enemyCount++;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public EnemyService()
    {
        _enemyController = new EnemyController(_enemyView, _enemySO);
    }

}
