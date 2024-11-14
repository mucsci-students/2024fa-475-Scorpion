using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject newPlayerPrefab; // The new player prefab (with the different sprite)

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that triggered the event has a SpriteRenderer (the player)
        if (other.GetComponent<SpriteRenderer>() != null)
        {
            // Get the position of the player object
            Vector3 playerPosition = other.transform.position;

            // Destroy the current player object
            Destroy(other.gameObject);

            // Instantiate the new player object at the same position
            Instantiate(newPlayerPrefab, playerPosition, Quaternion.identity);

            // Optionally, destroy the pickup object
            Destroy(gameObject);
        }
    }
}