using System;
using UnityEngine;

public class PowerUpView : MonoBehaviour
{
    public PowerUpType PowerUpType { get; private set; }
    public float Duration { get; private set; }

    public event Action OnApplyPowerUp;

    public void Initialize(PowerUpType powerUpType, float duration)
    {
        PowerUpType = powerUpType;
        Duration = duration;
    }

    public void ApplyPowerUp()
    {
        OnApplyPowerUp?.Invoke();
        Destroy(gameObject);
    }
}
