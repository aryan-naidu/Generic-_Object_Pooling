using UnityEngine;

public class PlayerController
{
    //Player related
    private PlayerView _playerView;
    private PlayerSO _playerSO;

    //Bullets Related
    private BulletView _bulletView;
    private BulletSO _bulletSO;
    private float _originalBulletSpeed;
    private float _currentBulletSpeed;
    private int _originalBulletDamage;
    private int _currentBulletDamage;

    public PlayerController(PlayerView playerView, PlayerSO playerSO, BulletSO bulletSO, BulletView bulletView)
    {
        _playerSO = playerSO;
        _bulletSO = bulletSO;
        _bulletView = bulletView;

        _originalBulletSpeed = _currentBulletSpeed = _bulletSO.Speed;
        _originalBulletDamage = _currentBulletDamage = _bulletSO.Damage;

        //Instantiate player at the center
        _playerView = GameObject.Instantiate(playerView);
        _playerView.transform.position = new Vector3(0, 0, 0);

        _playerView.OnCursorMove += RotatePlayer;
        _playerView.OnPressShoot += FireBullet;
        _playerView.OnDispose += UnSubscribe;

        // Bullet related
        GameService.instance.OnIncreaseBulletSpeed += IncreaseBulletSpeed;
        GameService.instance.OnIncreaseBulletDamage += IncreaseBulletDamage;
        GameService.instance.OnResetBulletSpeed += ResetBulletSpeed;
        GameService.instance.OnResetBulletDamage += ResetBulletDamage;
    }

    #region Player Input Functionality
    private void RotatePlayer(Vector3 mousePosition)
    {
        Vector3 direction = mousePosition - _playerView.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle to face the cursor correctly
        angle += 90f;

        _playerView.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FireBullet(Transform outTransform)
    {
        new BulletController(_bulletView, _bulletSO, outTransform);
    }
    #endregion

    #region Powerups Related
    private void IncreaseBulletSpeed(float value)
    {
        _currentBulletSpeed += value;
        _bulletSO.Speed = _currentBulletSpeed;
    }

    private void IncreaseBulletDamage(int value)
    {
        _currentBulletDamage += value;
        _bulletSO.Damage = _currentBulletDamage;
    }

    private void ResetBulletSpeed()
    {
        _currentBulletSpeed = _bulletSO.Speed = _originalBulletSpeed;
    }

    private void ResetBulletDamage()
    {
        _currentBulletDamage = _bulletSO.Damage = _originalBulletDamage;
    }
    #endregion

    // UnSubscribe
    public void UnSubscribe()
    {
        _playerView.OnCursorMove -= RotatePlayer;
        _playerView.OnPressShoot -= FireBullet;
        _playerView.OnDispose -= UnSubscribe;

        GameService.instance.OnIncreaseBulletSpeed -= IncreaseBulletSpeed;
        GameService.instance.OnIncreaseBulletDamage -= IncreaseBulletDamage;
        GameService.instance.OnResetBulletSpeed -= ResetBulletSpeed;
        GameService.instance.OnResetBulletDamage -= ResetBulletDamage;
    }
}
