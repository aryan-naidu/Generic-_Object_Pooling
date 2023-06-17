using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private PowerUpView _powerUpView;
    private PowerUpSO _powerUpSO;
    private PowerUpType _powerUpType;

    public PowerUpController(PowerUpView powerUpView, PowerUpSO powerUpSO)
    {
        _powerUpView = powerUpView;
        _powerUpSO = powerUpSO;
        _powerUpType = _powerUpSO.PowerUptype;

        _powerUpView.SetController(this);
    }

    public void ApplyPowerUp()
    {
        switch (_powerUpType)
        {
            case PowerUpType.DoubleCannon:
                IncreaseDamage();
                break;

            case PowerUpType.RapidBullets:
                IncreaseBulletSpeed();
                break;

            case PowerUpType.Shield:
                ActivateSheild();
                break;     
        }
    }

    private void IncreaseDamage()
    {
        GameService.instance.OnIncreaseDamage?.Invoke(_powerUpSO.ExtraDamage,_powerUpSO.Timer);
    }

    private void IncreaseBulletSpeed()
    {
        GameService.instance.OnIncreaseBulletSpeed?.Invoke(_powerUpSO.BulletSpeed,_powerUpSO.Timer);
    }

    private void ActivateSheild()
    {
        GameService.instance.OnActivateSheild?.Invoke(_powerUpSO.Timer);
    }
}
