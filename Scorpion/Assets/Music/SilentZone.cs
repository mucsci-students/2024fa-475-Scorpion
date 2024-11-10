using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilentZone : MonoBehaviour
{

    public int numPlayersInSilentZone;

    void OnTriggerEnter2D (Collider2D c)
    {
        if (c.tag == "Player1" || c.tag == "Player2")
            ++numPlayersInSilentZone;
    }

    void OnTriggerExit2D (Collider2D c)
    {
        if (c.tag == "Player2" || c.tag == "Player1")
            --numPlayersInSilentZone;
    }
}
