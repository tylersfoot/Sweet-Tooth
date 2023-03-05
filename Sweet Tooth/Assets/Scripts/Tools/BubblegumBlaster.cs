using UnityEngine;

public class BubblegumBlaster : MonoBehaviour
{
    public GameObject projectilePrefab; // loads in projectile prefab
    public Transform projectileSpawn; // loads in the projectile spawn GameObject

    public float shootForce = 50f; // initial speed of projectile
    public float damage = 10f; // damage of projectile
    public float fireRate = 0.5f; // fire rate of projectile
    public float range = 100f; // how far the projectile travel
    private float nextFire = 0f; // idk wtf this is lol

    // array of colors for the gumball
    public Color[] colors = new Color[] {Color.red, Color.green, Color.blue, Color.yellow};



    public void Shoot()
    {
        // spawn a new projectile at the shoot point
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation, GameObject.Find("Projectiles").transform);
        // apply a force to the projectile in the shoot point's forward direction
        Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(projectileSpawn.forward * shootForce, ForceMode.Impulse);

        // get the Renderer component of the new projectile
        Renderer projectileRenderer = newProjectile.GetComponent<Renderer>();
        // choose a random color from the colors array
        Color randomColor = colors[Random.Range(0, colors.Length)];
        // set the color of the projectile to the random color
        projectileRenderer.material.color = randomColor;

        // if (Time.time >= nextFire)
        // {
        //     nextFire = Time.time + fireRate;
            // Perform shooting logic here, such as creating a projectile or raycast
        // }
    }
}
