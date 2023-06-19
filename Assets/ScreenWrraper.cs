using UnityEngine;

public class ScreenWrapper
{
    private Camera _mainCamera;
    private Vector2 _screenBounds;

    public ScreenWrapper()
    {
        _mainCamera = Camera.main;
        _screenBounds = _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.transform.position.z));
    }

    public void WrapAroundScreen(Transform transformToWrap)
    {
        Vector3 newPosition = transformToWrap.position;
        bool hasWrapped = false;

        if (Mathf.Abs(transformToWrap.position.x) > _screenBounds.x)
        {
            newPosition.x = -Mathf.Sign(transformToWrap.position.x) * _screenBounds.x;
            hasWrapped = true;
        }

        if (Mathf.Abs(transformToWrap.position.y) > _screenBounds.y)
        {
            newPosition.y = -Mathf.Sign(transformToWrap.position.y) * _screenBounds.y;
            hasWrapped = true;
        }

        if (hasWrapped)
        {
            transformToWrap.position = newPosition;
        }
    }
}
