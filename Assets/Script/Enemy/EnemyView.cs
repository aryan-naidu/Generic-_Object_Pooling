using System;
using System.Collections;
using UnityEngine;

public class EnemyView : MonoBehaviour,IDamageble
{
    [SerializeField] private GameObject _explosion;
    private int _currentHealth;
    private Rigidbody rgbd;

    public Action<Rigidbody,EnemyView> MoveEnemy;
    public Action OnDead;
    public bool IsUsed;

    private void OnEnable()
    {
        _explosion.SetActive(false);
        rgbd = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        MoveEnemy?.Invoke(rgbd,this);
    }

    public void Damage(float value)
    {
        _currentHealth -= (int)value;
        if (_currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    public void SetHealth(int health)
    {
        _currentHealth = health;
    }

    private void DestroyEnemy()
    {
        GetComponent<BoxCollider>().enabled = false;
        // Explode and destroy object
        _explosion.SetActive(true);
        GameService.Instance.StartCoroutine(DestroyEnemyObject());

        OnDead?.Invoke();

        // For Updating the gameplay UI
        GameService.Instance.GetEnemyService()._enemiesDestroyedCount++;
        GameService.Instance.OnEnemiesKilled?.Invoke(GameService.Instance.GetEnemyService()._enemiesDestroyedCount);
    }

    private IEnumerator DestroyEnemyObject()
    {
        yield return new WaitForSeconds(0.5f);
        EnemyPool.ReturnEnemy(this);
    }
}
