using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// spawns a sword hitbox when a key is pressed, tells it how much damage to do, and then deletes after a duration
public class SwordSwing : MonoBehaviour
{
    public GameObject hitboxPrefab; 
    public KeyCode swingButton = KeyCode.Z; 
    public float swingDuration = 0.5f; 
    public int damageAmount = 5; 
    public Vector2 hitboxOffset = new Vector2(1f, 0f); 

    private PlayerMovement playerMovement; 
    private bool isSwinging = false; 

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Get the player movement script
    }

    void Update()
    {
        if (Input.GetKeyDown(swingButton) && !isSwinging && !playerMovement.isAttacking)
        {
            StartCoroutine(SwingSword());
        }
    }

    IEnumerator SwingSword()
    {
        isSwinging = true;
        playerMovement.isAttacking = true;

        // disabling player movement causes the player to keep moving in the same direction
        // this is kind of cool, but we might decide to remove this later --LCC
        playerMovement.enabled = false;

        
        Vector2 spawnPosition = (Vector2)transform.position + playerMovement.lastFacingDirection * hitboxOffset.magnitude + new Vector2(0f, 0.25f); // offset y pos by 0.5 -- LCC
        GameObject hitbox = Instantiate(hitboxPrefab, spawnPosition, Quaternion.identity, transform); // made sword swing child of player --LCC

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
        playerMovement.isAttacking = false;
    }
    public void IncreaseDamage(int amount)
    {
        damageAmount += amount; // Increase the damage by the specified amount
    }
}
