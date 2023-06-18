using System;
using UnityEngine;

public class BulletController
{
    private BulletView _bullet;
    private BulletSO _bulletSO;

    public BulletController(BulletView bulletView, BulletSO bulletSO)
    {
        _bulletSO = bulletSO;
        _bullet = bulletView;

        Subscribe();
    }

    public void Setup()
    {
        _bullet.gameObject.SetActive(true);
        Subscribe();
    }

    private void Subscribe()
    {
        _bullet.OnBulletDispose += OnDispose;
    }

    public void Launch(Transform outTransform)
    {
        _bullet.transform.position = outTransform.position;
        _bullet.transform.rotation = outTransform.rotation * Quaternion.Euler(0f, 0f, 180f);
        _bullet.SetDamageValue(_bulletSO.Damage);

        Rigidbody bulletRigidbody = _bullet.GetComponent<Rigidbody>();
        Vector3 direction = -outTransform.up;
        bulletRigidbody.velocity = direction * _bulletSO.Speed;

        // For updating the gameplay UI
        GameService.instance.GetPlayerService().OnBulletFired();
    }

    public void OnDispose()
    {
        _bullet.gameObject.SetActive(false);
        BulletPool.ReturnBullet(this);
        _bullet.OnBulletDispose -= OnDispose;
    }
}