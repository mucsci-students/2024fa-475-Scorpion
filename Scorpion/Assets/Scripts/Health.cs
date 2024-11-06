using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private bool isEnemy = false;  // Set this to true only for enemies

    private int currHealth;
    private int lastHitByPlayerID;

    public GameObject coinPrefab;

    void Start()
    {
        currHealth = maxHealth;
    }

    public int GetHealth()
    {
        return currHealth;
    }

    public bool TakeDamage(int amt, int playerID)
    {
        currHealth -= amt;
        lastHitByPlayerID = playerID;

        print(gameObject.name + " remaining health: " + currHealth);

        if (currHealth <= 0)
        {
            Die();
            return false;
        }
        return true;
    }

    public bool TakeDamage(int amt, List<string> targets)
    {
        if (targets.Contains(gameObject.tag))
            return TakeDamage(amt, lastHitByPlayerID);

        int halvedAmt = amt / 2;
        return TakeDamage(halvedAmt * 2 < amt ? halvedAmt + 1 : halvedAmt, lastHitByPlayerID);
    }

    public void HealDamage(int amt)
    {
        currHealth += amt;
        if (currHealth > maxHealth)
            currHealth = maxHealth;
    }

    public void Die()
    {
        // Only spawn a coin if this is an enemy
        if (isEnemy && lastHitByPlayerID != 0)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Coin>().Initialize(lastHitByPlayerID);
        }

        Destroy(gameObject);
    }
}
