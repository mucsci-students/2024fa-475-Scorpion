using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDamagePowerUp : MonoBehaviour
{
    public int damageIncreaseAmount = 1; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            PlayerMovement playerMovement = collision.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.IncreaseArrowDamage(damageIncreaseAmount); 
                Destroy(gameObject); 
            }
        }
    }
}