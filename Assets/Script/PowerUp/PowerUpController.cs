using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PowerUpController
{
    private PowerUpView _powerUpView;
    private PowerUpSO _powerUpSO;
    private GameService _gameService;

    private float _minSpawnDistanceFromPlayer = 3f;
    public 
        Action<PowerUpType> OnPowerUpExpired;

    public PowerUpController(PowerUpView powerUpView, PowerUpSO powerUpSO)
    {
        _powerUpView = GameObject.Instantiate(powerUpView);
        _powerUpView.transform.position = GetRandomSpawnPoint();
        _powerUpSO = powerUpSO;
        _gameService = GameService.Instance;

        _powerUpView.ActivatePowerUp += ActivatePowerUp;
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
        if (distanceFromPlayer < _minSpawnDistanceFromPlayer)
        {
            randomPoint += direction.normalized * (_minSpawnDistanceFromPlayer - distanceFromPlayer);
        }

        return randomPoint;
    }

    public void ActivatePowerUp()
    {
        _powerUpView.ActivatePowerUp -= ActivatePowerUp;

      _gameService.StartCoroutine(PowerUpCoroutine());
    }

    private IEnumerator PowerUpCoroutine()
    {
        switch (_powerUpSO.PowerUptype)
        {
            case PowerUpType.DoubleCannon:
                GameService.Instance.OnIncreaseBulletDamage?.Invoke(_powerUpSO.ExtraDamage);
                break;

            case PowerUpType.RapidBullets:
                GameService.Instance.OnIncreaseBulletSpeed?.Invoke(_powerUpSO.BulletSpeed);
                break;

            case PowerUpType.Shield:
                GameService.Instance.OnActivateSheild?.Invoke();
                break;
        }

        yield return new WaitForSeconds(_powerUpSO.Timer);

        switch (_powerUpSO.PowerUptype)
        {
            case PowerUpType.DoubleCannon:
                GameService.Instance.OnResetBulletDamage?.Invoke();
                break;

            case PowerUpType.RapidBullets:
                GameService.Instance.OnResetBulletSpeed?.Invoke();
                break;

            case PowerUpType.Shield:
                GameService.Instance.OnDeactivateShield?.Invoke();
                break;
        }

        if (OnPowerUpExpired != null)
        {
            OnPowerUpExpired?.Invoke(_powerUpSO.PowerUptype);
        }
    }
}
