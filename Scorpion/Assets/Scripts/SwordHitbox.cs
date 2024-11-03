using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// waits 0.1 seconds and then does damage to everything it hit, except for anything using a shield
public class SwordHitbox : MonoBehaviour
{
    public int DamageAmount { get; set; } // Damage amount set by SwordSwing script
    public List<string> validTargets = new List<string> { "Enemy" }; // Tags for valid targets
    
    private List<GameObject> enemiesToDamage = new List<GameObject> (); // enemies in the hitbox
    private List<GameObject> enemiesNotToDamage = new List<GameObject> (); // enemies shielded
    private float timeUntilDoDamage = 0.1f; // in seconds

    private float timeOfSpawn;
    private bool damageDone = false;

    
    void Start ()
    {
        timeOfSpawn = Time.time;
    }

    void Update ()
    {
        if (timeOfSpawn + timeUntilDoDamage < Time.time && !damageDone)
        {
            DoDamage ();
            damageDone = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Health enemyHealth = collision.collider.GetComponent<Health>();
        GameObject other = collision.gameObject;
        if (enemyHealth != null)
        {
            enemiesToDamage.Add (other); // damage this enemy in a few milliseconds
        }
        else if (other.CompareTag("Shield"))
        {
            enemiesNotToDamage.Add (other.GetComponent<Shield> ().wielder); // actually don't damage this enemy, it's shielded
        }
    }

    // damage all of the enemies in the enemiesToDamage list
    private void DoDamage ()
    {
        foreach (GameObject g in enemiesToDamage)
        {
            if (!enemiesNotToDamage.Contains (g))
                g.GetComponent<Health> ().TakeDamage (DamageAmount, validTargets);
        }
    }
}