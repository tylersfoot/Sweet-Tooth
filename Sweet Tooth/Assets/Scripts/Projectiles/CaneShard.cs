using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneShard : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
    }
}
