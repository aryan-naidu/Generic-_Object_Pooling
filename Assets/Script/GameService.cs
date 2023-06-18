using System;
using UnityEngine;

public class GameService : SingletonGeneric<GameService>
{
    [SerializeField] private PlayerService _playerService;
    [SerializeField] private EnemyService _enemyService;
    [SerializeField] private PowerUpService _powerUpService;
    [SerializeField] private SoundService _soundService;
    [SerializeField] private VfxService _vfxService;
    [SerializeField] private UiView _uiView;

    public Action SpawnEnemy;
    // Powerups Related
    public Action<int> OnIncreaseBulletDamage;
    public Action<float> OnIncreaseBulletSpeed;
    public Action OnResetBulletDamage;
    public Action OnResetBulletSpeed;
    public Action OnActivateSheild;
    public Action OnDeactivateShield;
    // Gameplay UI Related
    public Action<int> OnBulletsFired;
    public Action<int> OnEnemiesSpawned;
    public Action<int> OnEnemiesKilled;

    public PlayerService GetPlayerService() => _playerService;
    public EnemyService GetEnemyService() => _enemyService;
    public PowerUpService GetPowerUpService() => _powerUpService;
    public SoundService GetSoundService() => _soundService;
    public VfxService GetVfxService() => _vfxService;
    public UiView GetUiView() => _uiView;
}
