using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // Reference to the arrow prefab
    public KeyCode shootButton = KeyCode.Space; // Button to shoot
    public float shootCooldown = 0.5f; // Time between shots
    private float lastShootTime = 0f; // Last time the player shot an arrow

    private PlayerMovement playerMovement;
    private Vector2 shootingDirection;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        shootingDirection = playerMovement.lastFacingDirection; // Initialize with the default facing direction
    }

    void Update()
    {
        if (Input.GetKey(shootButton) && Time.time > lastShootTime + shootCooldown)
        {
            // Update the shooting direction from the latest facing direction every frame while shooting
            shootingDirection = playerMovement.lastFacingDirection;
            ShootArrow();
            lastShootTime = Time.time; // Update last shoot time
        }
    }

    void ShootArrow()
    {
        // Instantiate the arrow prefab
        GameObject arrow = Instantiate(arrowPrefab, transform.position + (Vector3) shootingDirection, Quaternion.identity);

        // Get the Arrow component and set its direction
        Arrow arrowComponent = arrow.GetComponent<Arrow>();
        arrowComponent.SetDirection(shootingDirection);

        // Rotate the arrow to match the shooting direction
        float angle = Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}