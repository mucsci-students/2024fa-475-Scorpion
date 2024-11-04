
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public float speedMultiplier = 1.5f; // How much to increase the player's speed
    

    void OnTriggerEnter2D(Collider2D other)
    {
   
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            
            playerMovement.ApplyPermanentSpeedBoost(speedMultiplier);

           
            Destroy(gameObject);
        }
    }
}
