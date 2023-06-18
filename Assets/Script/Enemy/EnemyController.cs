using System.Collections;
using UnityEngine;

public class EnemyController
{
    private Rigidbody _enemyRgbd;
    private Vector3 _target;
    private EnemySO _enemySO;
    private EnemyView _enemyView;

    public EnemyController(EnemyView enemy, EnemySO enemyScriptableObject)
    {
        _enemySO = enemyScriptableObject;

        Vector3 spawnPosition = GetRandomEdgeSpawnPosition();
        _enemyView = GameObject.Instantiate(enemy, spawnPosition, Quaternion.identity);
        _enemyRgbd = _enemyView.GetComponent<Rigidbody>();

        // Because the player(target) is always at the center
        _target = new Vector3(0, 0, 0);

        _enemyView.OnDamage += ApplyDamage;
        _enemyView.MoveEnemy += MoveTowardsTarget;
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

    private void MoveTowardsTarget()
    {
        Vector3 direction = _target - _enemyView.transform.position;
        direction.Normalize();

        // Calculate the rotation angle to face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smoothly rotate the enemy towards the target direction
        _enemyRgbd.MoveRotation(Quaternion.RotateTowards(_enemyView.transform.rotation, targetRotation, 180f));

        // Move the enemy in the direction of the target
        _enemyRgbd.velocity = direction * 3f;
    }

    public void ApplyDamage(int value)
    {
        _enemySO.Health -= value;

        if (_enemySO.Health <= 0)
        {
            EnemyDestroyed();
        }
    }

    private void EnemyDestroyed()
    {
        _enemyView.GetComponent<BoxCollider>().enabled = false;

        // Explode and destroy object
        _enemyView.Explode();
        GameService.instance.StartCoroutine(DestroyEnemyObject());

        // For Updating the gameplay UI
        GameService.instance.GetEnemyService()._enemiesDestroyedCount++;
        GameService.instance.OnEnemiesKilled?.Invoke(GameService.instance.GetEnemyService()._enemiesDestroyedCount);

        // Unsubscribe
        _enemyView.OnDamage -= ApplyDamage;
        _enemyView.MoveEnemy -= MoveTowardsTarget;
    }

    private IEnumerator DestroyEnemyObject()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject.Destroy(_enemyView.gameObject);
    }
}