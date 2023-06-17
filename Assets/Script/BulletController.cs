using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : IBullet
{
    [SerializeField] private float speed = 30f;

    private BulletView _bullet;
    private ScriptableObject _bulletSO;

    public BulletController(BulletView bulletView,ScriptableObject bulletSO)
    {
        _bullet = bulletView;
        _bulletSO = bulletSO;
    }

    public void Shoot(Vector2 direction,float speed)
    {
        direction.Normalize();
        _bullet.transform.up = direction;
        _bullet.GetComponent<Rigidbody>().velocity = direction * speed;
    }

}
