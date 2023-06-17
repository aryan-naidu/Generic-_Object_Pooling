using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : IBullet
{
    private float _originalSpeed;
    private float _currentSpeed;
    private int _originalDamage;
    private int _currentDamage;

    private BulletView _bullet;
    private BulletSO _bulletSO;

    public BulletController(BulletView bulletView, BulletSO bulletSO)
    {
        _bullet = bulletView;
        _bulletSO = bulletSO;
        _originalSpeed = _currentSpeed = _bulletSO.Speed;
        _originalDamage=_currentDamage = _bulletSO.Damage;

        GameService.instance.OnIncreaseBulletSpeed += IncreaseSpeed;
        GameService.instance.OnIncreaseDamage += IncreaseDamage;
    }

    public void Shoot(Transform transform)
    {
        BulletView bulletObject =GameObject.Instantiate(_bullet, transform.position, transform.rotation*Quaternion.Euler(0f,0f,180f));
        Rigidbody bulletRigidbody = bulletObject.GetComponent<Rigidbody>();
        Vector3 direction = -transform.up; // Get the direction from the shooter's up vector
        bulletRigidbody.velocity = direction * _currentSpeed;
    }

    private void IncreaseSpeed(float value, int timer)
    {
        _currentSpeed += value;
    }

    private void IncreaseDamage(int value, int timer)
    {
        _currentDamage += value;
    }

}
