using System;
using UnityEngine;

public class PowerUpController
{
    private PowerUpView _powerUpView;
    private PowerUpTimeController _powerUpTimeController;

    public event Action<PowerUpType, float> ActivatePowerUp;
    public event Action<PowerUpController> DeactivatePowerUp;

    public PowerUpController(PowerUpView powerUpView, PowerUpSO powerUpSO)
    {
        _powerUpView = GameObject.Instantiate(powerUpView);

        _powerUpView.Initialize(powerUpSO.PowerUptype, powerUpSO.Timer);
        _powerUpView.transform.position = GetRandomSpawnPosition(new Vector3(0, 0, 0), 2f);
        _powerUpView.OnApplyPowerUp += ApplyPowerUp;

        _powerUpTimeController = new PowerUpTimeController();
    }
    private Vector3 GetRandomSpawnPosition(Vector3 playerPosition, float minDistanceFromPlayer)
    {
        // Get the screen boundaries in world space
        Vector3 screenMin = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 screenMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        Vector3 spawnPosition = Vector3.zero;
        bool isValidSpawnPosition = false;

        // Generate a random position until a valid one is found
        while (!isValidSpawnPosition)
        {
            // Generate a random position within the screen boundaries
            spawnPosition = new Vector3(UnityEngine.Random.Range(screenMin.x, screenMax.x), UnityEngine.Random.Range(screenMin.y, screenMax.y), 0f);

            // Calculate the distance between the spawn position and the player position
            float distanceFromPlayer = Vector3.Distance(spawnPosition, playerPosition);

            // Check if the distance is greater than the minimum required distance
            if (distanceFromPlayer > minDistanceFromPlayer)
            {
                isValidSpawnPosition = true;
            }
        }

        return spawnPosition;
    }


    private void ApplyPowerUp()
    {
        // Get power-up type and duration from the PowerUpView or PowerUpSO
        PowerUpType powerUpType = _powerUpView.PowerUpType;
        float duration = _powerUpView.Duration;

        ActivatePowerUp?.Invoke(powerUpType, duration);
    }

    public void StartTimer(float duration)
    {
        _powerUpTimeController.StartTimer(duration);
    }

    public void StopTimer()
    {
        _powerUpTimeController.StopTimer();
        DeactivatePowerUp?.Invoke(this);
    }

    public void UpdateTimer()
    {
        _powerUpTimeController.UpdateTimer();
    }
}