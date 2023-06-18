using System;
using UnityEngine;

public class EnemyView : MonoBehaviour, IDamageble
{
    [SerializeField] private GameObject _explosion;

    public Action<int> OnDamage;
    public Action MoveEnemy;

    private void Update()
    {
        // tell controller to apply move logic
        MoveEnemy?.Invoke();
    }

    public void Damage(float value)
    {
        OnDamage((int)value);
    }

    public void Explode()
    {
        _explosion.SetActive(true);
    }
}
