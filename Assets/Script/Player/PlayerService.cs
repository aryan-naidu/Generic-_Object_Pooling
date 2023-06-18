using UnityEngine;

public class PlayerService : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private BulletView _bulletView;
    [SerializeField] private PlayerSO _playerSO;
    [SerializeField] private BulletSO _bulletSO;

    private PlayerController _playerController;

    private int _bulletsFired;

    public void Start()
    {
        BulletPool.Initialize(_bulletView, _bulletSO);
        _playerController = new PlayerController(_playerView, _playerSO);
        _playerController.OnBulletFired += OnBulletFired;
    }

    public void OnBulletFired()
    {
        _bulletsFired++;
        GameService.instance.OnBulletsFired?.Invoke(_bulletsFired);
    }

    private void OnDestroy()
    {
        _playerController.OnBulletFired -= OnBulletFired;
    }
}