using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeanutBrittle : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.CompareTag("Enemy"))
        {
            other.SendMessage("TakeDamage", damage);
            Destroy(gameObject);
        }
    }
}
