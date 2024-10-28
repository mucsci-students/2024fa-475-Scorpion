using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pressureplate : MonoBehaviour
{
    private bool inContactP1 = false;
    private bool inContactP2 = false;
    public bool playOnePlate = false;
    public bool playTwoPlate = false;
    private bool isActive = false;
    private Renderer plateRenderer;
    public Color activeColor = Color.blue;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        plateRenderer = GetComponent<Renderer>();
        originalColor = plateRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (inContactP1 && playOnePlate){
            plateRenderer.material.color = activeColor;
            isActive = true;
        }
        
        else if (inContactP2 && playTwoPlate){
            plateRenderer.material.color = activeColor;
            isActive = true;
        }
        else {
            isActive = false;
            plateRenderer.material.color = originalColor;
        }
    }

    public bool isitActive(){
        return isActive;
    }

    private void setPlayer(int x){
        if (x == 1){
            playOnePlate = true;
        }
        if (x == 2){
            playTwoPlate = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player1")){
            inContactP1 = true;
        }

        if (collision.gameObject.CompareTag("Player2")){
            inContactP2 = true;
        }
    }

    private void onTriggerStay2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player1")){

        }
        
        if (collision.gameObject.CompareTag("Player2")){
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Player1")){
            inContactP1 = false;
        }

        if (collision.gameObject.CompareTag("Player2")){
            inContactP2 = false;
        }
    }
}
