using System;
using UnityEngine;

public class PowerUpView : MonoBehaviour
{
    public Action ActivatePowerUp;

    public void ApplyPowerUp()
    {
        ActivatePowerUp?.Invoke();
        Destroy(gameObject);
    }
}
