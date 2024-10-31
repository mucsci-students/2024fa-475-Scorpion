using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    public float speed = 10f;               // Speed of the arrow
    public float destroyAfter = 5f;         // Time before the arrow disappears if it doesn’t hit anything
    public int minimalDamage = 2;           // Damage amount outside full damage zone
    public int fullDamage = 5;              // Damage amount inside full damage zone
    public List<string> validTargets = new List<string> { "Player", "Enemy" }; // Tags for valid targets

    private Rigidbody2D rb;
    private Vector2 direction;               // Direction of the arrow
    private PlayerCombat playerCombat;       // Reference to player combat script

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;     // Set the initial movement of the arrow
        Destroy(gameObject, destroyAfter);    // Destroy after a set time if no collision occurs

        // Locate the player combat script to check if in full damage zone
        playerCombat = FindObjectOfType<PlayerCombat>();
    }

    // Method to set the direction of the arrow
    public void SetDirection(Vector2 shootDirection)
    {
        direction = shootDirection.normalized; // Set and normalize the direction
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            // Reverse the arrow's direction by flipping its velocity
            rb.velocity = -rb.velocity;

            // Flip the sprite horizontally
            Vector3 flippedScale = transform.localScale;
            flippedScale.x *= -1;
            transform.localScale = flippedScale;

            Debug.Log("Arrow hit the shield, reversed direction, and flipped sprite.");
        }
        else if (validTargets.Contains(other.tag))
        {
            // Apply damage based on whether the player is in the full damage zone
            int damageAmount = playerCombat != null && playerCombat.isInFullDamageZone ? fullDamage : minimalDamage;

            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damageAmount);
                Debug.Log("Arrow hit " + other.name + " and dealt " + damageAmount + " damage.");
            }

            // Destroy the arrow after hitting a valid target
            Destroy(gameObject);
        }
        else
        {
            // Ignore non-target hits
            Debug.Log("Arrow hit non-target object: " + other.name + " and ignored it.");
        }
    }
}