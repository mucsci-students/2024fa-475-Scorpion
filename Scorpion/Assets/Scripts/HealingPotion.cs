using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    public int healingAmount = 5; 


    void OnTriggerEnter2D(Collider2D other)
    {
 
        Health playerHealth = other.GetComponent<Health>();

        if (playerHealth != null)
        {
         
            playerHealth.HealDamage(healingAmount);

            Destroy(gameObject); 
        }
    }
}
