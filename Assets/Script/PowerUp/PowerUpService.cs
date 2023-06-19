using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpService
{
    private PowerUpViewList _powerUpViewList;
    private PowerUpScriptableList _powerUpSOList;

    // Using custom data structure where we will get the powerup with least duration ramaining.
    private PriorityQueueCustom<PowerUpData> _powerUpQueue = new PriorityQueueCustom<PowerUpData>();

    public PowerUpService(PowerUpViewList powerUpViewList, PowerUpScriptableList powerUpSOList, float spawnInterval)
    {
        _powerUpViewList = powerUpViewList;
        _powerUpSOList = powerUpSOList;

        InitializePowerUpPool();

        new PowerUpController(_powerUpViewList, _powerUpSOList, spawnInterval);
    }

    private void InitializePowerUpPool()
    {
        Dictionary<PowerUpType, PowerUpView> dict = new Dictionary<PowerUpType, PowerUpView>();

        for(int i = 0 ; i < _powerUpViewList.PowerUps.Count; i++)
        {
            dict.Add(_powerUpSOList.PowerUpSoList[i].PowerUptype, _powerUpViewList.PowerUps[i]);
        }

        PowerUpPool.Initialize(dict);
    }
}
