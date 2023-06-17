using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : IBullet
{
    private float _speed;

    private BulletView _bullet;
    private BulletSO _bulletSO;

    public BulletController(BulletView bulletView,BulletSO bulletSO)
    {
        _bullet = bulletView;
        _bulletSO = bulletSO;
        _speed = _bulletSO.Speed;
    }

    public void Shoot(Transform transform)
    {
        BulletView bulletObject =GameObject.Instantiate(_bullet, transform.position, transform.rotation*Quaternion.Euler(0f,0f,180f));
        Rigidbody bulletRigidbody = bulletObject.GetComponent<Rigidbody>();
        Vector3 direction = -transform.up; // Get the direction from the shooter's up vector
        bulletRigidbody.velocity = direction * _speed;
    }

}
