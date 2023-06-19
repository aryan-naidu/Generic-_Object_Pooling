public class PlayerService
{
    private PlayerController _playerController;
    private BulletController _bulletController;

    private int _bulletsFired;
   
    public PlayerService(PlayerView playerView, PlayerSO playerSO,BulletView bulletView, BulletSO bulletSO)
    {
        BulletPool.Initialize(bulletView);

        _bulletController = new BulletController(bulletSO);
        _playerController = new PlayerController(playerView, playerSO, _bulletController);

        _playerController.OnBulletFired += OnBulletFired;
    }

    public void OnBulletFired()
    {
        _bulletsFired++;
        GameService.Instance.OnBulletsFired?.Invoke(_bulletsFired);
    }

    private void OnDestroy()
    {
        _playerController.OnBulletFired -= OnBulletFired;
    }
}