using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int player1Coins = 0;  // Coin count for player 1
    public int player2Coins = 0;  // Coin count for player 2

    // Method to increase coin count for a player
    public void AddCoins(int playerID, int amount)
    {
        if (playerID == 1)
        {
            player1Coins += amount;
            Debug.Log("Player 1's coins: " + player1Coins);
        }
        else if (playerID == 2)
        {
            player2Coins += amount;
            Debug.Log("Player 2's coins: " + player2Coins);
        }
    }
}
