using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour,IDamageble
{
    [SerializeField] Transform _shooter;
    private Transform _cachedTransform;
    private Camera _mainCamera;
    private PlayerController _playerController;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _cachedTransform = transform;
    }

    private void Update()
    {
        RotateTowardsMousePosition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameService.Instance.OnPressShoot?.Invoke(_shooter);
        }
    }

    private void RotateTowardsMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle to face the cursor correctly
        angle += 90f;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Damage(float value)
    {

    }
}
