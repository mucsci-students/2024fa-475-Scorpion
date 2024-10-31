using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public GameObject hitboxPrefab; 
    public KeyCode swingButton = KeyCode.Z; 
    public float swingDuration = 0.5f; 
    public int damageAmount = 1; 
    public Vector2 hitboxOffset = new Vector2(1f, 0f); 

    private PlayerMovement playerMovement; 
    private bool isSwinging = false; 

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Get the player movement script
    }

    void Update()
    {
        if (Input.GetKeyDown(swingButton) && !isSwinging)
        {
            StartCoroutine(SwingSword());
        }
    }

    IEnumerator SwingSword()
    {
        isSwinging = true;

        // Stop player movement during the swing
        playerMovement.enabled = false;

        
        Vector2 spawnPosition = (Vector2)transform.position + playerMovement.lastFacingDirection * hitboxOffset.magnitude;
        GameObject hitbox = Instantiate(hitboxPrefab, spawnPosition, Quaternion.identity);

        float angle = Mathf.Atan2(playerMovement.lastFacingDirection.y, playerMovement.lastFacingDirection.x) * Mathf.Rad2Deg;
        hitbox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Apply damage to any enemies in the hitbox
        SwordHitbox swordHitbox = hitbox.GetComponent<SwordHitbox>();
        if (swordHitbox != null)
        {
            swordHitbox.DamageAmount = damageAmount;
        }

        // Ensure the hitbox has a Collider2D component set for collision
        Collider2D collider = hitbox.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = false; // Ensure it's set for collision, not trigger
        }

        // Wait for the swing duration
        yield return new WaitForSeconds(swingDuration);

        // Enable player movement again
        playerMovement.enabled = true;

        // Destroy the hitbox
        Destroy(hitbox);

        isSwinging = false;
    }
}
