using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteringTextCollider : MonoBehaviour
{
    [SerializeField] private ScaryText text;

    private bool triggered = false;

    void OnTriggerEnter2D (Collider2D c)
    {
        if (!triggered && (c.tag == "Player1" || c.tag == "Player2"))
        {
            text.PlayEnteringSequence ();
            triggered = true;
        }
    }
}
