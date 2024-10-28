using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public GameObject swordPrefab;         // Assign your sword GameObject prefab here
    public KeyCode swingButton = KeyCode.Space;  // Customize this to your desired swing button
    public float spawnDistance = 1f;       // Distance in front of the player where sword appears
    public float swingDuration = 0.2f;     // Duration the sword remains visible
    public float cooldownTime = 1f;        // Cooldown time between swings
    public int damageAmount = 10;          // Amount of damage dealt per swing

    private GameObject activeSword;
    private bool isSwinging = false;
    private float nextSwingTime = 0f;

    void Update()
    {
        // Check if the swing button is pressed and if the cooldown period has passed
        if (Input.GetKeyDown(swingButton) && Time.time >= nextSwingTime)
        {
            SwingSword();
            nextSwingTime = Time.time + cooldownTime; // Set next allowed swing time
        }
    }

    void SwingSword()
    {
        // Calculate spawn position based on player's facing direction
        Vector3 spawnPosition = transform.position + transform.right * spawnDistance;

        // Spawn the sword prefab and orient it based on player's rotation
        activeSword = Instantiate(swordPrefab, spawnPosition, transform.rotation);

        // Add collision handling script to the sword prefab
        SwordCollider swordCollider = activeSword.AddComponent<SwordCollider>();
        swordCollider.damageAmount = damageAmount;

        // Destroy the sword after a short duration to simulate a swing
        Destroy(activeSword, swingDuration);
    }
}

// Separate class for handling sword collision
public class SwordCollider : MonoBehaviour
{
    public int damageAmount;

    void OnTriggerEnter2D(Collider2D other)  // Use OnTriggerEnter if using 3D physics
    {
        // Check if the other object has a Health component
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damageAmount);  // Apply damage
        }
    }
}