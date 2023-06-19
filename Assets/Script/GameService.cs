using System;
using UnityEngine;

public class GameService : SingletonGeneric<GameService>
{
    // Player related
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private BulletView _bulletView;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private BulletSO _bulletSO;

    // Enemy Realted
    [SerializeField] private EnemySO _enemySO;
    [SerializeField] private EnemyView _enemyView;
    [SerializeField] private int _initialDelayInSpawning = 2;
    [SerializeField] private int _delayBetweenSpawning = 2;

    // PowerUps Related
    [SerializeField] private PowerUpViewList _powerUpViewList;
    [SerializeField] private PowerUpScriptableList _powerUpSOList;
    [SerializeField] private float spawnInterval = 1f;

    // Sound Related
    [SerializeField] private SoundScriptableList _soundScriptableList;

    // Gameplay UI
    [SerializeField] private UiView _uiView;

    private PlayerService _playerService;
    private EnemyService _enemyService;
    private PowerUpService _powerUpService;
    private SoundService _soundService;
    private VfxService _vfxService;


    #region Actions

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
    #endregion

    private void Start()
    {
        InitializeServices();
    }
    private void Update()
    {
        _powerUpService.OnUpdate();
    }
    private void InitializeServices()
    {
        _playerService = new PlayerService(_playerView, _playerSO, _bulletView, _bulletSO);
        _enemyService = new EnemyService(_enemySO, _enemyView, _initialDelayInSpawning, _delayBetweenSpawning);
        _powerUpService = new PowerUpService(_powerUpViewList, _powerUpSOList, spawnInterval);
        _soundService = new SoundService(_soundScriptableList);
        _vfxService = new VfxService();
        _uiView = _uiView.GetComponent<UiView>();
    }

    public PlayerService GetPlayerService() => _playerService;
    public EnemyService GetEnemyService() => _enemyService;
    public PowerUpService GetPowerUpService() => _powerUpService;
    public SoundService GetSoundService() => _soundService;
    public VfxService GetVfxService() => _vfxService;
    public UiView GetUiView() => _uiView;
}
