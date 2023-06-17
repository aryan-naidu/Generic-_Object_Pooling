using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private BulletView _bulletView;
    [SerializeField] private ScriptableObject _playerSO;
    [SerializeField] private ScriptableObject _bulletSO;

    private PlayerController _playerController;
    private BulletController _bulletController;
    public PlayerController GetPlayerController() => _playerController;

    public PlayerService()
    {
        _bulletController = new BulletController(_bulletView, _bulletSO);
        _playerController = new PlayerController(_playerView, _playerSO, _bulletController);
    }

}
