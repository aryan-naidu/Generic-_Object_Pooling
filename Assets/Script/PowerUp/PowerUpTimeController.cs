using UnityEngine;

public class PowerUpTimeController
{
    private float _currentDuration;
    private bool _isTimerActive;

    public bool IsTimerActive => _isTimerActive;

    public void StartTimer(float duration)
    {
        if (_isTimerActive)
        {
            StopTimer();
        }

        _isTimerActive = true;
        _currentDuration = duration;

        // Notify relevant controllers that the power-up is active
        // For example: GameService.Instance.OnIncreaseBulletSpeed?.Invoke(powerUp.Speed);
    }

    public void StopTimer()
    {
        if (!_isTimerActive)
        {
            return;
        }

        // Notify relevant controllers that the power-up has expired
        // For example: GameService.Instance.OnResetBulletSpeed?.Invoke();

        _isTimerActive = false;
    }

    public void UpdateTimer()
    {
        if (!_isTimerActive)
        {
            return;
        }

        _currentDuration -= Time.deltaTime;

        if (_currentDuration <= 0f)
        {
            StopTimer();
        }
    }
}