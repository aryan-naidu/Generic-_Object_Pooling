using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool
{
    private static Dictionary<PowerUpType, ObjectPool<PowerUpView>> _powerUpPoolDictionary;

    public static void Initialize(Dictionary<PowerUpType, PowerUpView> powerUpDictionary)
    {
        _powerUpPoolDictionary = new Dictionary<PowerUpType, ObjectPool<PowerUpView>>();

        foreach (var kvp in powerUpDictionary)
        {
            PowerUpType powerUpType = kvp.Key;
            PowerUpView powerUpPrefab = kvp.Value;

            ObjectPool<PowerUpView> powerUpPool = new ObjectPool<PowerUpView>(powerUpPrefab);
            _powerUpPoolDictionary.Add(powerUpType, powerUpPool);
        }
    }

    public static PowerUpView GetPowerUp(PowerUpType powerUpType)
    {
        if (_powerUpPoolDictionary.ContainsKey(powerUpType))
        {
            return _powerUpPoolDictionary[powerUpType].GetObject();
        }

        return null; // Powerup type not found in the pool
    }

    public static void ReturnPowerUp(PowerUpView powerUp, PowerUpType powerUpType)
    {
        if (_powerUpPoolDictionary.ContainsKey(powerUpType))
        {
            _powerUpPoolDictionary[powerUpType].ReturnObject(powerUp);
        }
        else
        {
            // Powerup type not found in the pool, destroy the object
            GameObject.Destroy(powerUp.gameObject);
        }
    }
}
