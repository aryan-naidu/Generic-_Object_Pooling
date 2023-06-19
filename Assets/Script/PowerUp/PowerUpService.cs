using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpService
{
    private List<PowerUpController> _powerUpControllers; 
    private PowerUpViewList _powerUpViewList;
    private PowerUpScriptableList _powerUpScriptableList;
    private float _spawnInterval;
    private PowerUpTimeController _powerUpTimeController;

    public PowerUpService(PowerUpViewList powerUpViewList, PowerUpScriptableList powerUpScriptableList, float spawnInterval)
    {
        _powerUpViewList = powerUpViewList;
        _powerUpScriptableList = powerUpScriptableList;
        _spawnInterval = spawnInterval;

        _powerUpTimeController = new PowerUpTimeController();
        _powerUpControllers = new List<PowerUpController>(); 

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
            int randomIndex = UnityEngine.Random.Range(0, powerUpCount);
            PowerUpView powerUpView = _powerUpViewList.PowerUps[randomIndex];
            PowerUpSO powerUpSO = _powerUpScriptableList.PowerUpSoList[randomIndex];

            PowerUpController powerUpController = new PowerUpController(powerUpView, powerUpSO);
            _powerUpControllers.Add(powerUpController); // Add the controller to the list

            powerUpController.ActivatePowerUp += ActivatePowerUp;

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void ActivatePowerUp(PowerUpType powerUpType, float duration)
    {
        PowerUpData powerUp = new PowerUpData(powerUpType, duration);
        _powerUpTimeController.StartTimer(powerUp.ExpirationTime);
    }

    public void OnUpdate()
    {
        foreach (PowerUpController powerUpController in _powerUpControllers)
        {
            powerUpController.UpdateTimer();
        }

        _powerUpTimeController.UpdateTimer();
    }
}
