using System;
using UnityEngine;

public class PowerUpView : MonoBehaviour
{
    public Action ActivatePowerUp;
    private PowerUpType _powerUpType;

    public void SetPowerUpType(PowerUpType powerUpType)
    {
        _powerUpType = powerUpType;
    }

    public void ApplyPowerUp()
    {
        PowerUpPool.ReturnPowerUp(this, _powerUpType);
    }
}
