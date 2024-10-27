using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private Transform player; // Reference to the player's transform

    // Method to initialize the shield with the player's transform
    public void Initialize(Transform playerTransform)
    {
        player = playerTransform;
    }

    void Update()
    {
        // Update shield position to stay in front of the player
        if (player != null)
        {
            // Move the shield to a position in front of the player
            Vector3 spawnPosition = player.position + player.right; // Adjust distance as needed
            transform.position = spawnPosition;
            transform.rotation = player.rotation; // Keep the shield facing the same direction as the player
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other object has a Health component (or any other component to block)
       // Health health = other.GetComponent<Health>();
       // if (health != null)
        {
            // Block the projectile or enemy (add your blocking logic here)
            Debug.Log("Blocked an incoming object: " + other.name);
            Destroy(other.gameObject); // Destroy the incoming object, if desired
        }
    }
}