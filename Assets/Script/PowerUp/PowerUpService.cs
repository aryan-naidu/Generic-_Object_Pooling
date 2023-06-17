using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpService : MonoBehaviour
{
    [SerializeField] private PowerUpViewList _powerUpViewList;
    [SerializeField] private PowerUpScriptableList _powerUpSO;

    private PowerUpController _powerUpController;

    public PowerUpController GetPowerUpController() => _powerUpController;

    public float spawnRadius = 8f;  // Radius within which the power-up will be spawned
    public float spawnOffset = 1f;  // Offset from the specified point
    public float spawnInterval = 1f;  // Time interval between power-up spawns

    private Vector3 spawnPoint;  // Point at which the power-up will be spawned
    private float timer;  // Timer to track the spawn interval

    private void Start()
    {
        spawnPoint = new Vector3(0, 0, 0);  // Set the spawn point to the position of the spawner object
        timer = spawnInterval;  // Set the initial timer value to spawnInterval, so the first spawn happens immediately
        _powerUpController = new PowerUpController(_powerUpViewList.PowerUps[0], _powerUpSO.PowerUpSoList[0]);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Check if the timer exceeds the spawn interval
        if (timer >= spawnInterval)
        {
            // Reset the timer
            timer = 0f;

            // Calculate a random position within the spawn radius
            Vector2 randomOffset = Random.insideUnitCircle.normalized * spawnRadius;

            // Calculate the spawn position with the specified offset
            Vector3 spawnPosition = spawnPoint + new Vector3(randomOffset.x, randomOffset.y, 0f) + spawnOffset * Vector3.up;

            // Spawn the power-up object
            Instantiate(_powerUpViewList.PowerUps[0], spawnPosition, Quaternion.identity);
        }
    }
}
