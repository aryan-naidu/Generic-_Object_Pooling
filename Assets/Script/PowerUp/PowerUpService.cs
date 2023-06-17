using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpService
{
    [SerializeField] private PowerUpViewList _powerUpViewList;
    [SerializeField] private PowerUpScriptableList _powerUpSO;

    private PowerUpController _powerUpController;

    public PowerUpController GetPowerUpController() => _powerUpController;

    public PowerUpService()
    {
        _powerUpController = new PowerUpController(_powerUpViewList.PowerUps[0], _powerUpSO.PowerUpSoList[0]);
    }
}
