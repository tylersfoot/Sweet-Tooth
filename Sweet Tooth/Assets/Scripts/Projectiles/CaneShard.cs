using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaneShard : MonoBehaviour
{
    public float damage;
    public float pierceCount;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.SendMessage("TakeDamage", damage);
            pierceCount += 1;
            if (pierceCount >= 3)
            {
                Destroy(gameObject);
            }
        }
    }
}
