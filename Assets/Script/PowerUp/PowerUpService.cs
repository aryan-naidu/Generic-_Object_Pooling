using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpService
{
    private PowerUpViewList _powerUpViewList;
    private PowerUpScriptableList _powerUpSOList;
    private float _spawnInterval;

    // Using custom data structure where we will get the powerup with least duration ramaining.
    private PriorityQueueCustom<PowerUpData> _powerUpQueue = new PriorityQueueCustom<PowerUpData>();

    public PowerUpService(PowerUpViewList powerUpViewList, PowerUpScriptableList powerUpSOList, float spawnInterval)
    {
        _powerUpViewList = powerUpViewList;
        _powerUpSOList = powerUpSOList;
        _spawnInterval = spawnInterval;

        StartSpawning();
    }

    private void StartSpawning()
    {
       GameService.Instance.StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        yield return new WaitForSeconds(_spawnInterval);

        while (true)
        {
            int powerUpCount = _powerUpViewList.PowerUps.Count;

            int randomIndex = Random.Range(0, powerUpCount);
            PowerUpView powerUpView = _powerUpViewList.PowerUps[randomIndex];
            PowerUpSO powerUpSO = _powerUpSOList.PowerUpSoList[randomIndex];

            PowerUpController powerUpController = new PowerUpController(powerUpView, powerUpSO);
            powerUpController.OnPowerUpExpired += OnPowerUpExpired;
            powerUpController.ActivatePowerUp();

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void OnPowerUpExpired(PowerUpType powerUpId)
    {
        ResetPowerUpEffects(powerUpId);
    }

    private void ResetPowerUpEffects(PowerUpType powerUpId)
    {
        // Reset the changed values after powerup timer ends
        switch (powerUpId)
        {
            case PowerUpType.RapidBullets: 
                GameService.Instance.OnResetBulletSpeed?.Invoke();
                break;

            case PowerUpType.DoubleCannon: 
                GameService.Instance.OnResetBulletDamage?.Invoke();
                break;

            case PowerUpType.Shield: 
                GameService.Instance.OnDeactivateShield?.Invoke();
                break;
        }
    }

    // When a power-up is activated
    public void ActivatePowerUp(PowerUpType powerUpId, float duration)
    {
        float expirationTime = Time.time + duration;
        PowerUpData powerUpData = new PowerUpData(powerUpId, expirationTime);

        _powerUpQueue.Enqueue(powerUpData);
    }

    public void OnUpdate()
    {
        HandlePowerUpExpiration();
    }

    private void HandlePowerUpExpiration()
    {
        float currentTime = Time.time;

        while (_powerUpQueue.Count > 0 && _powerUpQueue.Peek().ExpirationTime <= currentTime)
        {
            PowerUpData expiredPowerUpData = _powerUpQueue.Dequeue();
            PowerUpType expiredPowerUpId = expiredPowerUpData.PowerUpType;

            OnPowerUpExpired(expiredPowerUpId);
        }
    }
}
