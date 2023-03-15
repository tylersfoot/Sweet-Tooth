using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeanutBrittleShotty : MonoBehaviour
{
    public GameObject projectilePrefab; // loads in projectile prefab
    public Transform projectileSpawn; // loads in the projectile spawn GameObject
    public PauseMenu pauseMenu; // for checking if game is paused
    public Tool tool; // for despawning projectiles
    public SoundManager soundManager; // for playing sound

    public float shootForce; // initial speed of projectile
    public float damage; // damage of projectile
    public float fireRate; // fire rate of projectile
    public float range; // how far the projectile travels
    public float lifespan; // how long until the projectile despawns
    public float spread; // max spread angle deviation in degrees
    public float shotAmount; // amount of shots to be fired
    public float currentAmmo; // how much ammo the player has
    public float maxAmmo; // max ammo
    private float fireCooldown = 0f; // time until next shot
    public bool isKeyDown = false;

    public AudioClip shootSound;

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
            for (int i = 0; i < shotAmount; i++)
            {
                Shoot();
                soundManager.PlaySound(shootSound);
                soundManager.PlaySound(shootSound);
            }
            // reset cooldown
            fireCooldown = Time.time + fireRate;
        }
    }

    public void Shoot()
    {
        // calculate random spread angles
        float spreadAngleX = Random.Range(-spread, spread);
        float spreadAngleY = Random.Range(-spread, spread);

        // create a random axis of rotation for the spread
        Vector3 axis = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        // apply the spread angles to the projectile's forward direction
        Quaternion spreadRotation = Quaternion.AngleAxis(spreadAngleX, Vector3.right) * Quaternion.AngleAxis(spreadAngleY, Vector3.up) * Quaternion.AngleAxis(spread, axis);
        Vector3 spreadVector = spreadRotation * projectileSpawn.forward;
        
        // spawn a new projectile at the shoot point
        GameObject newProjectile = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.identity, GameObject.Find("Projectiles").transform);
        // apply a force to the projectile in the shoot point's forward direction with randomized spread
        Rigidbody projectileRb = newProjectile.GetComponent<Rigidbody>();
        projectileRb.AddForce(spreadVector * shootForce, ForceMode.Impulse);

        // destroy the projectile after the specified lifespan
        tool.Despawn(newProjectile, lifespan);
    }
}
