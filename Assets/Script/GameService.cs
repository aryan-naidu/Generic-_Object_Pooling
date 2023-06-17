using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : SingletonGeneric<GameService>
{
    private PlayerService _playerServicce;
    private EnemyService _enemyService;
    private PowerUpService _powerUpService;
    private SoundService _soundService;
    private VfxService _vfxService;
    private UiView _uiView;

    public PlayerService GetPlayerService() => _playerServicce;
    public EnemyService GetEnemyService() => _enemyService;
    public PowerUpService GetPowerUpService() => _powerUpService;
    public SoundService GetSoundService() => _soundService;
    public VfxService GetVfxService() => _vfxService;
    public UiView GetUiView() => _uiView;

    private void Start()
    {
        _playerServicce = new PlayerService();
        _enemyService = new EnemyService();
        _powerUpService = new PowerUpService();
        _soundService = new SoundService();
        _vfxService = new VfxService();
        _uiView = new UiView();
    }

}
