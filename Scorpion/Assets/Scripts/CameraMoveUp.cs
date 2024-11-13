using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraMoveUp : MonoBehaviour
{
    public Transform player1; // Reference to player 1
    public Transform player2; // Reference to player 2
    public float targetY; // Y position where the camera should stop
    public float moveSpeed = 2f; // Speed of camera movement

    private PlayerMovement playerMovement1;
    private PlayerMovement playerMovement2;
    private bool isMoving = true; // Start moving automatically

    private void Start()
    {
        // Get the PlayerMovement scripts for both players
        playerMovement1 = player1.GetComponent<PlayerMovement>();
        playerMovement2 = player2.GetComponent<PlayerMovement>();

        // Disable player movement initially
        if (playerMovement1 != null) playerMovement1.enabled = false;
        if (playerMovement2 != null) playerMovement2.enabled = false;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveCameraUp();
        }
    }

    private void MoveCameraUp()
    {
        // Calculate the target position using the average x-position of both players
        Vector3 targetPosition = new Vector3((player1.position.x + player2.position.x) / 2, targetY, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Check if the camera has reached the target Y position
        if (Mathf.Abs(transform.position.y - targetY) < 0.01f)
        {
            isMoving = false;

            // Re-enable player movement
            if (playerMovement1 != null) playerMovement1.enabled = true;
            if (playerMovement2 != null) playerMovement2.enabled = true;
        }
    }
}
