using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    public GameObject hitboxPrefab; // The invisible hitbox prefab
    public KeyCode swingButton = KeyCode.Z; // Button to swing the sword
    public float swingDuration = 0.5f; // Duration the player is frozen
    public int damageAmount = 1; // Damage dealt to enemies in the hitbox
    public Vector2 hitboxOffset = new Vector2(1f, 0f); // Offset in front of the player

    private PlayerMovement playerMovement; // Reference to the player movement script
    private bool isSwinging = false; // Checks if the player is currently swinging

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

        // Instantiate the hitbox in front of the player based on lastFacingDirection
        Vector2 spawnPosition = (Vector2)transform.position + playerMovement.lastFacingDirection * hitboxOffset.magnitude;
        GameObject hitbox = Instantiate(hitboxPrefab, spawnPosition, Quaternion.identity);

        // Rotate the hitbox to face the player's last direction
        float angle = Mathf.Atan2(playerMovement.lastFacingDirection.y, playerMovement.lastFacingDirection.x) * Mathf.Rad2Deg;
        hitbox.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Apply damage to any enemies in the hitbox
        SwordHitbox swordHitbox = hitbox.GetComponent<SwordHitbox>();
        if (swordHitbox != null)
        {
            swordHitbox.DamageAmount = damageAmount;
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