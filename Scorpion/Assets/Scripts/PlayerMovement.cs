using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public KeyCode leftButton = KeyCode.A;
    public KeyCode rightButton = KeyCode.D;

    private Rigidbody2D rb;
    [HideInInspector]
    public Vector2 lastFacingDirection = Vector2.right; // Default facing direction
    private SpriteRenderer rend;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(upButton))
            movement.y += 1;
        if (Input.GetKey(downButton))
            movement.y -= 1;
        if (Input.GetKey(leftButton))
            movement.x -= 1;
        if (Input.GetKey(rightButton))
            movement.x += 1;

        if (movement != Vector2.zero)
        {
            movement.Normalize();
            rb.velocity = movement * moveSpeed;

            // Update lastFacingDirection every frame if there's movement
            lastFacingDirection = movement;

            // Flip the player only on the X-axis when moving left or right
            if (movement.x != 0)
            {
                rend.flipX = movement.x < 0;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}