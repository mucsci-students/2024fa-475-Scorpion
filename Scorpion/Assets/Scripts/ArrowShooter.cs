using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawns an arrow when a key is pressed, facing the correct direction
public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // Reference to the arrow prefab
    public KeyCode shootButton = KeyCode.Space; // Button to shoot
    public float shootCooldown = 0.5f; // Time between shots
    private float lastShootTime = 0f; // Last time the player shot an arrow
    private bool isShooting = false;

    private PlayerMovement playerMovement;
    private Vector2 shootingDirection;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        shootingDirection = playerMovement.lastFacingDirection; // Initialize with the default facing direction
    }

    void Update()
    {
        if (isShooting && Time.time > lastShootTime + shootCooldown)
        {
            isShooting = false;
            playerMovement.isAttacking = false;
        }
        if (Input.GetKey(shootButton) && !isShooting && !playerMovement.isAttacking)
        {
            // Update the shooting direction from the latest facing direction every frame while shooting
            shootingDirection = playerMovement.lastFacingDirection;
            ShootArrow();
            lastShootTime = Time.time; // Update last shoot time
            playerMovement.isAttacking = true;
            isShooting = true;
        }
    }

    void ShootArrow()
    {
        // Instantiate the arrow prefab (added offset of 0.25 in the y)
        GameObject arrow = Instantiate(arrowPrefab, transform.position + new Vector3 (0f, 0.25f, 0f) + 0.5f * (Vector3) shootingDirection, Quaternion.identity);

        // Get the Arrow component and set its direction
        Arrow arrowComponent = arrow.GetComponent<Arrow>();
        arrowComponent.SetDirection(shootingDirection);

        // Rotate the arrow to match the shooting direction
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}