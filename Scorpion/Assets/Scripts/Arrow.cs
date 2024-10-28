using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f; // Speed of the arrow
    public float destroyAfter = 5f; // Time before the arrow disappears if it doesn’t hit anything
    public int damageAmount = 1; // Damage amount the arrow deals
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // Initial movement of the arrow
        Destroy(gameObject, destroyAfter); // Destroy after a set time if no collision occurs
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
        else if (other.CompareTag("Enemy"))
        {
            // Apply damage if the arrow hits an enemy
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                Debug.Log("Arrow hit an enemy and dealt " + damageAmount + " damage.");
            }

            // Destroy the arrow after hitting the enemy
            Destroy(gameObject);
        }
        else
        {
            // Destroy the arrow on contact with anything else
            Debug.Log("Arrow hit: " + other.name + " and was destroyed.");
            Destroy(gameObject);
        }
    }
}