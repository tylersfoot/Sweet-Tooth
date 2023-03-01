using UnityEngine;

public class BubblegumBlaster : MonoBehaviour
{
    public float damage = 10f;
    public float fireRate = 0.5f;
    public float range = 100f;

    private float nextFire = 0f;

    public void Shoot()
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + fireRate;
            // Perform shooting logic here, such as creating a projectile or raycast
        }
    }
}
