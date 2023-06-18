using System.Collections;
using TMPro;
using UnityEngine;

public class UiView : MonoBehaviour
{
    [SerializeField] private TMP_Text _bulletsFiredCountText;
    [SerializeField] private TMP_Text _enemiesSpawnedCountText;
    [SerializeField] private TMP_Text _enemiesKilledCountText;

    [SerializeField] private GameObject _gameOverScreen;

    private void Start()
    {
        OnBulletsFired(0);
        OnEnemiesKilled(0);
        OnEnemiesSpawned(0);

        GameService.instance.OnBulletsFired += OnBulletsFired;
        GameService.instance.OnEnemiesSpawned += OnEnemiesSpawned;
        GameService.instance.OnEnemiesKilled += OnEnemiesKilled;
    }

    private void OnBulletsFired(int count)
    {
        _bulletsFiredCountText.text = count.ToString();
    }

    private void OnEnemiesSpawned(int count)
    {
        _enemiesSpawnedCountText.text = count.ToString();
    }

    private void OnEnemiesKilled(int count)
    {
        _enemiesKilledCountText.text = count.ToString();
    }

    public void EnableGameOverScreen()
    {
        _gameOverScreen.SetActive(true);
        StartCoroutine(QuitApplication());
    }

    private IEnumerator QuitApplication()
    {
        Time.timeScale = 0;
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
