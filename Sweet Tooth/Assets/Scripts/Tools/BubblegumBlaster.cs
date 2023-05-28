using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblegumBlaster : MonoBehaviour
{
    public GameObject projectilePrefab; // loads in projectile prefab
    public Transform projectileSpawn; // loads in the projectile spawn GameObject
    public PauseMenu pauseMenu; // for checking if game is paused
    public Tool tool; // for despawning projectiles
    public SoundManager soundManager; // for playing sound

    public float shootForce; // initial speed of projectile
    public float damage; // damage of projectile
    public float fireRate; // fire rate of projectile
    public float lifespan; // how long until the projectile despawns
    public float spread; // max spread angle deviation in degrees
    public float currentAmmo; // how much ammo the player has
    public float maxAmmo; // max ammo
    private float fireCooldown; // time until next shot
    public bool isKeyDown; // is the shoot key being held down

    public AudioClip shootSound;

    // array of colors for the gumball
    public Color[] colors = new Color[] {Color.red, Color.green, Color.blue, Color.yellow};

    void Start()
    {
        currentAmmo = maxAmmo;
        gameObject.SetActive(false);
    }

    void Update()
    {
        // if key is being pressed, cooldown is done, and not paused
        if (isKeyDown && Time.time >= fireCooldown && !pauseMenu.isPaused && currentAmmo > 0)
        {
            currentAmmo -= 1;
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
        Vector3 spreadVector = Quaternion.Euler(spreadAngleX, spreadAngleY, 0f) * Vector3.forward;

        // spawn a new projectile at the shoot point
        GameObject newProjectile = Instantiate(
            projectilePrefab,
            projectileSpawn.position,
            projectileSpawn.rotation,
            GameObject.Find("Projectiles").transform
        );
        newProjectile.SetActive(true);
        // apply a force to the projectile in the shoot point's forward direction with randomized spread
        Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();
        projectileRb.AddRelativeForce(spreadVector * shootForce, ForceMode.Impulse);

        // get the Renderer component of the new projectile
        Renderer projectileRenderer = newProjectile.GetComponent<Renderer>();
        // choose a random color from the colors array
        Color randomColor = colors[Random.Range(0, colors.Length)];
        // set the color of the projectile to the random color
        projectileRenderer.material.color = randomColor;

        newProjectile.GetComponent<Gumball>().damage = damage; // sets damage of the projectile

        // destroy the projectile after the specified lifespan
        tool.Despawn(newProjectile, lifespan);

        soundManager.PlaySound(shootSound);
    }

}
