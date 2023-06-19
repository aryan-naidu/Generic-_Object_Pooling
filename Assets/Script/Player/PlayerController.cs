using System;
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

    private BulletController _bulletController;

    public Action OnBulletFired;
    private GameService _gameService;

    public PlayerController(PlayerView playerView, PlayerSO playerSO, BulletController bulletController)
    {
        _bulletController = bulletController;
        _playerSO = playerSO;
        _gameService = GameService.Instance;
        _playerView = GameObject.Instantiate(playerView);
        _playerView.transform.position = new Vector3(0, 0, 0);
              
        // Events happening with the player
        _playerView.OnCursorMove += RotatePlayer;
        _playerView.OnPressShoot += FireBullet;
        _playerView.OnDispose += Dispose;

        _originalBulletSpeed = _currentBulletSpeed = _playerSO.BulletSO.Speed;
        _originalBulletDamage = _currentBulletDamage = _playerSO.BulletSO.Damage;

        // Bullet related
        _gameService.OnIncreaseBulletSpeed += IncreaseBulletSpeed;
        _gameService.OnIncreaseBulletDamage += IncreaseBulletDamage;
        _gameService.OnResetBulletSpeed += ResetBulletSpeed;
        _gameService.OnResetBulletDamage += ResetBulletDamage;
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
        _bulletController.Launch(outTransform);
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

        _gameService.OnIncreaseBulletSpeed -= IncreaseBulletSpeed;
        _gameService.OnIncreaseBulletDamage -= IncreaseBulletDamage;
        _gameService.OnResetBulletSpeed -= ResetBulletSpeed;
        _gameService.OnResetBulletDamage -= ResetBulletDamage;
    }
}