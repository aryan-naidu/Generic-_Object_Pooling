using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "powerUp")
        {
            collision.gameObject.GetComponent<PowerUpView>().ApplyPowerUp();
        }
    }
}
