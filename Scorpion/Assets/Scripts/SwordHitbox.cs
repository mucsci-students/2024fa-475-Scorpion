using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public int DamageAmount { get; set; } // Damage amount set by SwordSwing script
    public List<string> validTargets = new List<string> { "Enemy" }; // Tags for valid targets

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Attempt to get the Health component on the enemy
        Health enemyHealth = collision.collider.GetComponent<Health>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(DamageAmount, validTargets);
        }
        else
        {

        }
    }
}