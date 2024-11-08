using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private bool isEnemy = false;  // Set this to true only for enemies

    private int currHealth;
    private int lastHitByPlayerID;
    private bool immune = false;
    private float immuneTime = 2.0f;
    private Vector3 respawnOffset = new Vector3(-5f, 0f, 0f);
    public GameObject coinPrefab;
    public Lives totalLives;
    public Camera mainCamera;

    void Start()
    {
        if (mainCamera == null) {
        //Debug.LogError("No Camera tagged as MainCamera found!");}
        currHealth = maxHealth;
        mainCamera = Camera.main;
        }
    }

    public int GetHealth()
    {
        return currHealth;
    }

    public bool TakeDamage(int amt, int playerID)
    {
        if(immune){
            return true;
        }
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

    public void disable(){
        immune = true;
        StartCoroutine(immunityTimer());
    }

    private IEnumerator immunityTimer(){
        yield return new WaitForSeconds(immuneTime);
        immune = false;
    }

    public void Die()
    {
        // Only spawn a coin if this is an enemy
        if (isEnemy && lastHitByPlayerID != 0)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Coin>().Initialize(lastHitByPlayerID);
            Destroy(gameObject);
        }   
            if(isEnemy == false){
            totalLives.reduceLives();
            if(!totalLives.isEmpty() && isEnemy == false && (gameObject.CompareTag("Player1") || gameObject.CompareTag("Player2"))){
                disable();
                Respawn();
                currHealth = maxHealth;
                
            }
            else{
        Destroy(gameObject);
            }
            }
    }

    private void Respawn() {
    int originalLayer = gameObject.layer;
    Vector3 viewportPosition = new Vector3(10.0f, 2.5f, mainCamera.nearClipPlane);

    if (gameObject.CompareTag("Player1")){
        viewportPosition = new Vector3(10.0f, 0.5f, mainCamera.nearClipPlane);
    } 
    
        
    
    Vector3 respawnPosition = mainCamera.ViewportToWorldPoint(viewportPosition);
    respawnPosition.z = transform.position.z;
    respawnPosition += respawnOffset;
    transform.position = respawnPosition;
    Vector3 currentScale = transform.localScale;
    gameObject.layer = originalLayer;
    transform.localScale = currentScale;
    }

    public void zeroHealth(){
        currHealth = 0;
    }
}
