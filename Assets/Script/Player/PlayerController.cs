using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private BulletController _bulletController;
    private PlayerView _playerView;
    private ScriptableObject _playerSO;

    public PlayerController(PlayerView playerView, ScriptableObject playerSO, BulletController bulletController)
    {
        _playerView = playerView;
        _playerSO = playerSO;
        _bulletController = bulletController;
    }
}
