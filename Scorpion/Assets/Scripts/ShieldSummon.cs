using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSummon : MonoBehaviour
{
    public GameObject shieldPrefab;          // Assign your shield GameObject prefab here
    public KeyCode summonButton = KeyCode.E; // Key to summon the shield

    private GameObject currentShield; // Reference to the current shield instance

    void Update()
    {
        // Check if the summon button is pressed
        if (Input.GetKeyDown(summonButton))
        {
            SummonShield();
        }
        // Check if the summon button is released
        else if (Input.GetKeyUp(summonButton))
        {
            DestroyShield();
        }
    }

    void SummonShield()
    {
        // Instantiate the shield at the player's position
        currentShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);

        // Initialize the shield with the player's transform
        Shield shieldScript = currentShield.GetComponent<Shield>();
        shieldScript.Initialize(transform);
    }

    void DestroyShield()
    {
        if (currentShield != null)
        {
            Destroy(currentShield); // Destroy the current shield instance
            currentShield = null;   // Reset the reference
        }
    }
}