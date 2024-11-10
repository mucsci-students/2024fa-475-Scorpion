using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureRoomCollider : MonoBehaviour
{
    [SerializeField] private ScaryText text;

    private bool triggered = false;

    void OnTriggerEnter2D (Collider2D c)
    {
        if (!triggered && (c.tag == "Player1" || c.tag == "Player2"))
        {
            text.PlayTreasureRoomSequence ();
            triggered = true;
        }
    }
}
