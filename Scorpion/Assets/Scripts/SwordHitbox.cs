using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public int DamageAmount { get; set; } // Damage amount set by SwordSwing script

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object hit has the "Enemy" tag
        if (collision.collider.CompareTag("Enemy"))
        {
            // Attempt to get the Health component on the enemy
            Health enemyHealth = collision.collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(DamageAmount); // Apply damage
                Debug.Log("Sword hit an enemy and dealt " + DamageAmount + " damage.");
            }
            else
            {
                Debug.LogWarning("Enemy hit, but no Health component found on " + collision.collider.name);
            }
        }
        else
        {
            Debug.Log("Sword hit: " + collision.collider.name + ", but it is not tagged as Enemy.");
        }
    }
}