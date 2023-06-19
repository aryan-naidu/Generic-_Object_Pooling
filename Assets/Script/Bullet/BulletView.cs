using System;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    private int _damageValue;
    public Action OnBulletDispose;
    public bool IsUsed;

   public void SetDamageValue(int damageValue)
    {
        _damageValue = damageValue;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            PowerUpView powerUpView = collision.gameObject.GetComponent<PowerUpView>();
            if (powerUpView != null)
            {
                powerUpView.ApplyPowerUp();
                BulletPool.ReturnBullet(this);
            }
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyView enemyView = collision.gameObject.GetComponent<EnemyView>();
            if (enemyView != null)
            {
                enemyView.Damage(_damageValue);
                BulletPool.ReturnBullet(this);
            }
        }
    }
}
