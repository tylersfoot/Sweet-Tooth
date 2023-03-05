using UnityEngine;

public class BubblegumBlaster : MonoBehaviour
{
    public GameObject projectilePrefab; // loads in projectile prefab
    public Transform projectileSpawn; // loads in the projectile spawn GameObject
    public float shootForce = 10f; // initial speed of projectile
    public float damage = 10f; // damage of projectile
    public float fireRate = 0.5f; // fire rate of projectile
    public float range = 100f; // how far the projectile travels

    private float nextFire = 0f; // idk wtf this is lol

    public void Shoot()
    {
         // spawn a new projectile at the shoot point
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);

        // apply a force to the projectile in the shoot point's forward direction
        Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(projectileSpawn.forward * shootForce, ForceMode.Impulse);

        // if (Time.time >= nextFire)
        // {
        //     nextFire = Time.time + fireRate;
            // Perform shooting logic here, such as creating a projectile or raycast
        // }
    }
}
