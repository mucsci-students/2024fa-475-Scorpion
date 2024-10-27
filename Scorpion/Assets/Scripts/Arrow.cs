using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f; // Speed of the arrow
    public float destroyAfter = 5f; // Time before the arrow disappears if it sticks
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed; // Move the arrow in the direction it's facing
        Destroy(gameObject, destroyAfter); // Destroy the arrow after the specified time
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the arrow hits an enemy (assuming enemies have a Health component)
       // Health health = other.GetComponent<Health>();
       // if (health != null)
        {
            // If it's an enemy, apply damage and destroy the arrow
         //   health.TakeDamage(1); // Change 1 to the desired damage amount
            Debug.Log("Arrow hit an enemy: " + other.name);
            Destroy(gameObject); // Destroy the arrow on hit
        }
        //else
        {
            // If it's not an enemy, stick the arrow to the object
            StickArrow(other);
        }
    }

    void StickArrow(Collider2D other)
    {
        // Stop the arrow's movement
        rb.velocity = Vector2.zero;

        // Disable the collider to prevent further collisions
        GetComponent<Collider2D>().enabled = false;

        // Set the arrow's parent to the object it hit to keep it in place
        transform.SetParent(other.transform);

        Debug.Log("Arrow stuck to: " + other.name);
        // No need to destroy the arrow here since it will be destroyed after the time set in Start
    }
}
