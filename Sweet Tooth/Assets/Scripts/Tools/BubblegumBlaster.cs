using UnityEngine;
using System.Collections;

public class BubblegumBlaster : MonoBehaviour
{
    public GameObject projectilePrefab; // loads in projectile prefab
    public Transform projectileSpawn; // loads in the projectile spawn GameObject

    public float shootForce; // initial speed of projectile
    public float damage; // damage of projectile
    public float fireRate; // fire rate of projectile
    public float range; // how far the projectile travels
    public float lifespan; // how long until the projectile despawns
    public float spread; // max spread angle deviation in degrees
    private float fireCooldown = 0f; // time until next shot
    public bool isKeyDown = false;

    // array of colors for the gumball
    public Color[] colors = new Color[] {Color.red, Color.green, Color.blue, Color.yellow};

    void Start()
    {
        // ! blaster does not have collider so not necessary (if it does in the future uncomment this)
        // get the colliders for the player and gumball
        // Collider playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();
        // Collider gumballCollider = projectilePrefab.GetComponent<Collider>();
        // ignore collisions between the player and gumball
        // Physics.IgnoreCollision(playerCollider, gumballCollider);
    }

    void Update()
    {
        if (isKeyDown && Time.time >= fireCooldown)
        {
            Shoot();

            // reset cooldown
            fireCooldown = Time.time + fireRate;
        }
    }

    public void Shoot()
    {
        // calculate random spread angles
        float spreadAngleX = Random.Range(-spread, spread);
        float spreadAngleY = Random.Range(-spread, spread);

        // apply the spread angles to the projectile's forward direction
        Vector3 spreadVector = Quaternion.Euler(spreadAngleX, spreadAngleY, 0f) * projectileSpawn.forward;
    
        // spawn a new projectile at the shoot point
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation, GameObject.Find("Projectiles").transform);
        // apply a force to the projectile in the shoot point's forward direction with randomized spread
        Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(spreadVector * shootForce, ForceMode.Impulse);

        // get the Renderer component of the new projectile
        Renderer projectileRenderer = newProjectile.GetComponent<Renderer>();
        // choose a random color from the colors array
        Color randomColor = colors[Random.Range(0, colors.Length)];
        // set the color of the projectile to the random color
        projectileRenderer.material.color = randomColor;

        // destroy the projectile after the specified lifespan
        StartCoroutine(DestroyAfterTime(newProjectile, lifespan));
    }

    private IEnumerator DestroyAfterTime(GameObject obj, float time)
    {
        // this function destroys the gumball after `delay` seconds
        yield return new WaitForSeconds(time);
        Destroy(obj);
    }
}
