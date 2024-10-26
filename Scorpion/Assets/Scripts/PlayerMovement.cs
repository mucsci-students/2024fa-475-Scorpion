using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
 
    public float speed = 5f;

    // Key bindings for movement
    public KeyCode moveUpKey = KeyCode.W;
    public KeyCode moveDownKey = KeyCode.S;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    private Vector2 moveDirection;

    void Update()
    {
        moveDirection = Vector2.zero;

        // Check for input based on assigned keys
        if (Input.GetKey(moveUpKey))
            moveDirection += Vector2.up;

        if (Input.GetKey(moveDownKey))
            moveDirection += Vector2.down;

        if (Input.GetKey(moveLeftKey))
            moveDirection += Vector2.left;

        if (Input.GetKey(moveRightKey))
            moveDirection += Vector2.right;

        // Move the player
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }
}
