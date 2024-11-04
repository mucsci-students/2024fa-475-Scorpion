using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamagePowerUp : MonoBehaviour
{
    public int damageIncreaseAmount = 5; // Amount to increase the sword damage

    void OnTriggerEnter2D(Collider2D other)
    {
       
        SwordSwing swordSwing = other.GetComponent<SwordSwing>();

        if (swordSwing != null)
        {
            
            swordSwing.IncreaseDamage(damageIncreaseAmount);

    
            Destroy(gameObject);
        }
    }
}
