using UnityEngine;

public class PlayerController
{
    //Player related
    private PlayerView _playerView;
    private PlayerSO _playerSO;

    //Bullets Related
    private float _originalBulletSpeed;
    private float _currentBulletSpeed;
    private int _originalBulletDamage;
    private int _currentBulletDamage;

    public event System.Action OnBulletFired;

    public PlayerController(PlayerView playerView, PlayerSO playerSO)
    {
        _playerSO = playerSO;
        _playerView = GameObject.Instantiate(playerView);
        _playerView.transform.position = new Vector3(0, 0, 0);

        _playerView.OnCursorMove += RotatePlayer;
        _playerView.OnPressShoot += FireBullet;
        _playerView.OnDispose += Dispose;

        _originalBulletSpeed = _currentBulletSpeed = _playerSO.BulletSO.Speed;
        _originalBulletDamage = _currentBulletDamage = _playerSO.BulletSO.Damage;

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
        angle += 90f;
        _playerView.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FireBullet(Transform outTransform)
    {
       BulletPool.GetBullet(outTransform);
    }

    #endregion

    #region Powerups Related
    private void IncreaseBulletSpeed(float value)
    {
        _currentBulletSpeed += value;
        _playerSO.BulletSO.Speed = _currentBulletSpeed;
    }

    private void IncreaseBulletDamage(int value)
    {
        _currentBulletDamage += value;
        _playerSO.BulletSO.Damage = _currentBulletDamage;
    }

    private void ResetBulletSpeed()
    {
        _currentBulletSpeed = _playerSO.BulletSO.Speed = _originalBulletSpeed;
    }

    private void ResetBulletDamage()
    {
        _currentBulletDamage = _playerSO.BulletSO.Damage = _originalBulletDamage;
    }
    #endregion

    public void Dispose()
    {
        _playerView.OnCursorMove -= RotatePlayer;
        _playerView.OnPressShoot -= FireBullet;
        _playerView.OnDispose -= Dispose;

        GameService.instance.OnIncreaseBulletSpeed -= IncreaseBulletSpeed;
        GameService.instance.OnIncreaseBulletDamage -= IncreaseBulletDamage;
        GameService.instance.OnResetBulletSpeed -= ResetBulletSpeed;
        GameService.instance.OnResetBulletDamage -= ResetBulletDamage;
    }
}