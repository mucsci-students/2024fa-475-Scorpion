using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the player
    public KeyCode upButton = KeyCode.W;      // Button for moving up
    public KeyCode downButton = KeyCode.S;    // Button for moving down
    public KeyCode leftButton = KeyCode.A;    // Button for moving left
    public KeyCode rightButton = KeyCode.D;   // Button for moving right

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 movement = Vector2.zero;

        // Check for movement input
        if (Input.GetKey(upButton))
            movement.y += 1;
        if (Input.GetKey(downButton))
            movement.y -= 1;
        if (Input.GetKey(leftButton))
            movement.x -= 1;
        if (Input.GetKey(rightButton))
            movement.x += 1;

        // Normalize the movement vector to avoid faster diagonal movement
        if (movement != Vector2.zero)
        {
            movement.Normalize();
            rb.velocity = movement * moveSpeed;

            // Rotate the player to face the direction of movement
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop the player if no movement input
        }
    }
}
