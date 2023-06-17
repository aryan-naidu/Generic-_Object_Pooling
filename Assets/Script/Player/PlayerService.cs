using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private BulletView _bulletView;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private BulletSO _bulletSO;

    private PlayerController _playerController;
    private BulletController _bulletController;
    public PlayerController GetPlayerController() => _playerController;

    public void Start()
    {
        _bulletController = new BulletController(_bulletView, _bulletSO);
        _playerController = new PlayerController(_playerView, _playerSO, _bulletController);
        var player = Instantiate(_playerView);
        player.transform.position = new Vector3(0, 0, 0);
    }
}
