using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PowerUpController
{
    private PowerUpViewList _powerUpViewList;
    private PowerUpScriptableList _powerUpSOList;
    private float _spawnInterval;
    private float _minSpawnDistanceFromPlayer = 3f;

    public PowerUpController(PowerUpViewList powerUpViewList,PowerUpScriptableList powerUpScriptableList, float spawnDelay)
    {
        _spawnInterval = spawnDelay;

        StartSpawning();

        _powerUpViewList = powerUpViewList;
        _powerUpSOList = powerUpScriptableList;
    }

    private void StartSpawning()
    {
        GameService.Instance.StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        yield return new WaitForSeconds(_spawnInterval);

        while (true)
        {
            int powerUpCount = _powerUpViewList.PowerUps.Count;

            int randomIndex = UnityEngine.Random.Range(0, powerUpCount);
            PowerUpType powerUpType = _powerUpSOList.PowerUpSoList[randomIndex].PowerUptype;
            PowerUpView powerUpView = PowerUpPool.GetPowerUp(powerUpType);
            powerUpView.transform.position = GetRandomSpawnPoint();

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        // Generate a random point within the screen bounds
        float x = UnityEngine.Random.Range(0f, Screen.width);
        float y = UnityEngine.Random.Range(0f, Screen.height);

        Vector3 randomPoint = new Vector3(x, y, 0f);

        // Convert the screen point to world coordinates
        randomPoint = Camera.main.ScreenToWorldPoint(randomPoint);
        randomPoint.z = 0f; // Set z position to 0

        // Calculate the direction from the player(0,0,0) to the random point
        Vector3 direction = randomPoint;
        float distanceFromPlayer = direction.magnitude;

        // Check if the random point is too close to the player, adjust if necessary
        if (distanceFromPlayer < _minSpawnDistanceFromPlayer)
        {
            randomPoint += direction.normalized * (_minSpawnDistanceFromPlayer - distanceFromPlayer);
        }

        return randomPoint;
    }
}
