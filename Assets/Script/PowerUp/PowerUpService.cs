using System.Collections;
using UnityEngine;

public class PowerUpService : MonoBehaviour
{
    [SerializeField] private PowerUpViewList _powerUpViewList;
    [SerializeField] private PowerUpScriptableList _powerUpSOList;
    [SerializeField] private float spawnInterval = 1f;


    private void Start()
    {
        StartCoroutine(SpawnPowerUps());
    }

    private IEnumerator SpawnPowerUps()
    {
        yield return new WaitForSeconds(spawnInterval);

        while (true)
        {
            int powerUpCount = _powerUpViewList.PowerUps.Count;
            int randomIndex = Random.Range(0, powerUpCount);
            PowerUpView powerUpView = _powerUpViewList.PowerUps[randomIndex];
            PowerUpSO powerUpSO = _powerUpSOList.PowerUpSoList[randomIndex];

            new PowerUpController(powerUpView, powerUpSO);
            yield return new WaitForSeconds(spawnInterval);
        }        
    }
}
