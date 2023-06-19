using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpData
{
    public PowerUpType PowerUpType { get; }
    public float ExpirationTime { get; }

    public PowerUpData(PowerUpType powerUpId, float expirationTime)
    {
        PowerUpType = powerUpId;
        ExpirationTime = expirationTime;
    }

}
