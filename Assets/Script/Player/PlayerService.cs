public class PlayerService
{
    private PlayerController _playerController;

    private int _bulletsFired;
   
    public PlayerService(PlayerView playerView, PlayerSO playerSO,BulletView bulletView, BulletSO bulletSO)
    {
        // Create the player
        _playerController = new PlayerController(playerView, playerSO, bulletSO,bulletView);
    }

    public void OnBulletFired()
    {
        _bulletsFired++;
        GameService.Instance.OnBulletsFired?.Invoke(_bulletsFired);
    }
}
