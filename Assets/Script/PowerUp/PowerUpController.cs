using System.Collections;
using UnityEngine;

public class PowerUpController
{
    private PowerUpView _powerUpView;
    private PowerUpSO _powerUpSO;
    private Coroutine _powerUpCoroutine;

    private float _minDistanceFromPlayer = 3f;

    public PowerUpController(PowerUpView powerUpView, PowerUpSO powerUpSO)
    {
        _powerUpView = GameObject.Instantiate(powerUpView);
        _powerUpView.transform.position = GetRandomSpawnPoint();
        _powerUpSO = powerUpSO;

        _powerUpView.ActivatePowerUp += ApplyPowerUp;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        // Generate a random point within the screen bounds
        float x = UnityEngine.Random.Range(0f, Screen.width);
        float y = UnityEngine.Random.Range(0f, Screen.height);

        Vector3 randomPoint = new Vector3(x, y, 0f);

        // Convert the screen point to world coordinates
        randomPoint = Camera.main.ScreenToWorldPoint(randomPoint);
        randomPoint.z = 0f; // Set z position to 0

        // Calculate the direction from the player(0,0,0) to the random point
        Vector3 direction = randomPoint;
        float distanceFromPlayer = direction.magnitude;

        // Check if the random point is too close to the player, adjust if necessary
        if (distanceFromPlayer < _minDistanceFromPlayer)
        {
            randomPoint += direction.normalized * (_minDistanceFromPlayer - distanceFromPlayer);
        }

        return randomPoint;
    }

    #region When PowerUp Activates
    public void ApplyPowerUp()
    {
        if (_powerUpCoroutine != null)
        {
            // If a power-up is already active, stop the coroutine before starting a new one
            GameService.Instance.StopCoroutine(_powerUpCoroutine);
        }

        // Unsubscribe
        _powerUpView.ActivatePowerUp += ApplyPowerUp;

        switch (_powerUpSO.PowerUptype)
        {
            case PowerUpType.DoubleCannon:
                _powerUpCoroutine = GameService.Instance.StartCoroutine(DoubleCannonPowerUpCoroutine());
                break;

            case PowerUpType.RapidBullets:
                _powerUpCoroutine = GameService.Instance.StartCoroutine(RapidBulletsPowerUpCoroutine());
                break;

            case PowerUpType.Shield:
                _powerUpCoroutine = GameService.Instance.StartCoroutine(ShieldPowerUpCoroutine());
                break;
        }
    }

    private IEnumerator DoubleCannonPowerUpCoroutine()
    {
        GameService.Instance.OnIncreaseBulletDamage?.Invoke(_powerUpSO.ExtraDamage);
        yield return new WaitForSeconds(_powerUpSO.Timer);

        // Reset damage to original value
        GameService.Instance.OnResetBulletDamage?.Invoke();
    }

    private IEnumerator RapidBulletsPowerUpCoroutine()
    {
        GameService.Instance.OnIncreaseBulletSpeed?.Invoke(_powerUpSO.BulletSpeed);
        yield return new WaitForSeconds(_powerUpSO.Timer);

        // Reset speed to original value
        GameService.Instance.OnResetBulletSpeed?.Invoke();
    }
    #endregion

    private IEnumerator ShieldPowerUpCoroutine()
    {
        GameService.Instance.OnActivateSheild?.Invoke();
        yield return new WaitForSeconds(_powerUpSO.Timer);

        // Deactivate the shield
        GameService.Instance.OnDeactivateShield?.Invoke();
    }
}
