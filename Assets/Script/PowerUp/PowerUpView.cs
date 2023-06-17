using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpView : MonoBehaviour
{
    private PowerUpController _powerUpController;
    public void ApplyPowerUp()
    {
        _powerUpController.ApplyPowerUp();
    }

    public void SetController(PowerUpController powerUpController)
    {
        _powerUpController = powerUpController;
    }
}
