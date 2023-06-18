using System;
using UnityEngine;

public class EnemyView : MonoBehaviour, IDamageble
{
    [SerializeField] private GameObject _explosion;

    public Action<int> OnDamage;
    public Action MoveEnemy;

    private void OnEnable()
    {
        _explosion.SetActive(false);
        GetComponent<BoxCollider>().enabled = true;
    }

    private void Update()
    {
        // tell controller to apply move logic
        MoveEnemy?.Invoke();
    }

    public void SetHealth(int value)
    {

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
