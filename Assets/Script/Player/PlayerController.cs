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

    private GameService _gameService;

    public PlayerController(PlayerView playerView, PlayerSO playerSO, BulletSO bulletSO, BulletView bulletView)
    {
        _playerSO = playerSO;
        _bulletSO = bulletSO;
        _bulletView = bulletView;
        _gameService = GameService.Instance;

        // Fo setting and resetting powerups effects
        _originalBulletSpeed = _currentBulletSpeed = _bulletSO.Speed;
        _originalBulletDamage = _currentBulletDamage = _bulletSO.Damage;

        //Instantiate player at the center
        _playerView = GameObject.Instantiate(playerView);
        _playerView.transform.position = new Vector3(0, 0, 0);
              
        // Events happening with the player
        _playerView.OnCursorMove += RotatePlayer;
        _playerView.OnPressShoot += FireBullet;
        _playerView.OnDispose += Unsubscribe;

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

    // Unsubscribe
    public void Unsubscribe()
    {
        _playerView.OnCursorMove -= RotatePlayer;
        _playerView.OnPressShoot -= FireBullet;
        _playerView.OnDispose -= Unsubscribe;

        _gameService.OnIncreaseBulletSpeed -= IncreaseBulletSpeed;
        _gameService.OnIncreaseBulletDamage -= IncreaseBulletDamage;
        _gameService.OnResetBulletSpeed -= ResetBulletSpeed;
        _gameService.OnResetBulletDamage -= ResetBulletDamage;
    }
}
