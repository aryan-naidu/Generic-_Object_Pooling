using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerView : MonoBehaviour, IDamageble
{
    [SerializeField] private Transform _bulletOriginTransform;
    [SerializeField] private GameObject _explosion;

    // For Notifying The Controller
    public Action<Vector3> OnCursorMove;
    public Action<Transform> OnPressShoot;
    public Action OnDispose;

    private void Update()
    {
        RotateTowardsMousePosition();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnPressSpace();
        }
    }

    #region Player Inputs
    private void RotateTowardsMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        OnCursorMove?.Invoke(mousePosition);
    }

    private void OnPressSpace()
    {
        OnPressShoot?.Invoke(_bulletOriginTransform);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
           // Explode();
        }
    }

    public void Explode()
    {
        _explosion.SetActive(true);
        StartCoroutine(GameOver());
    }

    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        GameService.instance.GetUiView().EnableGameOverScreen();
    }

    public void Damage(float value)
    {

    }

    private void OnDestroy()
    {
        OnDispose?.Invoke();
    }
}
