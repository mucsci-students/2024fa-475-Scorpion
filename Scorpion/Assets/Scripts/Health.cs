using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [SerializeField] private int maxHealth = 100;

    private int currHealth;

    void Start()
    {
        currHealth = maxHealth;
    }

    public int GetHealth ()
    {
        return currHealth;
    }
    
    // does damage to the object, killing it if its health drops to zero
    // returns true if the object survives
    public bool TakeDamage (int amt)
    {
        currHealth -= amt;
        if (currHealth <= 0)
        {
            Die ();
            return false;
        }
        print (gameObject.name + " remaining health: " + currHealth);
        return true;
    }

    // does damage to the object, but only half damage if this object is not the intended target
    public bool TakeDamage (int amt, List<string> targets)
    {
        if (targets.Contains (gameObject.tag))
            return TakeDamage (amt);
        return TakeDamage (amt / 2);
    }

    // heals damage from the object if the object isn't at max health
    public void HealDamage (int amt)
    {
        currHealth += amt;
        if (currHealth > maxHealth)
            currHealth = maxHealth;
    }

    public void Die ()
    {
        Destroy (gameObject);
    }
}
