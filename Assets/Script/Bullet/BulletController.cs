using UnityEngine;

public class BulletController : IBullet
{
    private BulletView _bullet;
    private BulletSO _bulletSO;

    public BulletController(BulletView bulletView, BulletSO bulletSO, Transform outTransform)
    {
        _bulletSO = bulletSO;
        _bullet = bulletView;

        InstantiateAndLaunch(outTransform);
    }

    public void InstantiateAndLaunch(Transform outTransform)
    {
        var bullet = GameObject.Instantiate(_bullet, outTransform.position, outTransform.rotation * Quaternion.Euler(0f, 0f, 180f));
        bullet.SetDamageValue(_bulletSO.Damage);

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

        // Get the direction from the shooter's up vector
        Vector3 direction = -outTransform.up;
        bulletRigidbody.velocity = direction * _bulletSO.Speed;

        // For updating the gameplay UI
        GameService.Instance.GetPlayerService().OnBulletFired();
    }
}
