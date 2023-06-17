using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    private PowerUpView _powerUpView;
    private PowerUpSO _powerUpSO;

    public PowerUpController(PowerUpView powerUpView, PowerUpSO powerUpSO)
    {
        _powerUpView = powerUpView;
        _powerUpSO = powerUpSO;
    }
}
