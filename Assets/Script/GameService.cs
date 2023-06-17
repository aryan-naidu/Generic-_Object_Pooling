using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameService : SingletonGeneric<GameService>
{
    [SerializeField] private PlayerService _playerService;
    [SerializeField] private EnemyService _enemyService;
    [SerializeField] private PowerUpService _powerUpService;
    [SerializeField] private SoundService _soundService;
    [SerializeField] private VfxService _vfxService;
    [SerializeField] private UiView _uiView;

    public Action<Vector3> OnCursorMove;
    public Action<Transform> OnPressShoot;
    public Action SpawnEnemy;
    public Action<int, int> OnIncreaseDamage;
    public Action<float, int> OnIncreaseBulletSpeed;
    public Action<int> OnActivateSheild;
    public PlayerService GetPlayerService() => _playerService;
    public EnemyService GetEnemyService() => _enemyService;
    public PowerUpService GetPowerUpService() => _powerUpService;
    public SoundService GetSoundService() => _soundService;
    public VfxService GetVfxService() => _vfxService;
    public UiView GetUiView() => _uiView;

}
