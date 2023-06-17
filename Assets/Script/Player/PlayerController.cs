using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private BulletController _bulletController;
    private PlayerView _playerView;
    private PlayerSO _playerSO;

    private float _rotationSpeed;

    public PlayerController(PlayerView playerView, PlayerSO playerSO, BulletController bulletController)
    {
        _playerView = playerView;
        _playerSO = playerSO;
        _bulletController = bulletController;
        _rotationSpeed = _playerSO.RotationSpeed;

        GameService.instance.OnCursorMove += RotatePlayer;
        GameService.instance.OnPressShoot += FireBullet;

    }

    private void RotatePlayer(Vector3 direction)
    {
       
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle to face the cursor correctly
        angle += 90f;

        _playerView.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void FireBullet(Transform outt)
    {
        _bulletController.Shoot(outt);
    }

    public Transform GetPlayerTransform()
    {
        return _playerView.transform;
    }
}
