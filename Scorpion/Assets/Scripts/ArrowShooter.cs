using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // Reference to the arrow prefab
    public KeyCode shootButton = KeyCode.Space; // Button to shoot
    public float shootForce = 20f; // Force applied to the arrow
    private float shootCooldown = 1f; // Time between shots
    private float lastShootTime = 0f; // Last time the player shot an arrow

    void Update()
    {
        // Check for shooting input
        if (Input.GetKey(shootButton) && Time.time > lastShootTime + shootCooldown)
        {
            ShootArrow();
            lastShootTime = Time.time; // Update last shoot time
        }
    }

    void ShootArrow()
    {
        // Instantiate the arrow prefab
        GameObject arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        // Set the arrow's velocity based on the player's facing direction
        rb.velocity = transform.right * shootForce;
    }
}