using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour, IDamageble
{
    private Vector3 target;
    private void Start()
    {
        target = new Vector3(0, 0, 0);
    }
    public void Damage(float value)
    {

    }
    private void Update()
    {
        Vector3 direction = target - transform.position;
        direction.Normalize();  // Normalize the direction vector to have a magnitude of 1

        transform.Translate(direction * 3 * Time.deltaTime);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Damage(1f);
        }
    }
}
