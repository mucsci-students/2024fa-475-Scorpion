using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public bool moving = false;     //To be used later for stop and go implementation
    public float camSpeed = 0.5f;
    public float boundHeight = 50f;
    public CamTrigger camTrigger;
    private bool inContact = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    void startMoving(){
        moving = true;
    }

    void increaseMax(float x){
        boundHeight += x;
    }
    void stopMoving(){
        moving = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        inContact = camTrigger.moveCam();
        Vector3 newPostion = transform.position + Vector3.up * camSpeed * Time.deltaTime;
        if (newPostion.y <= boundHeight && moving){
            transform.position = newPostion;
        }
        //if(Input.GetMouseButtonDown(0)){
        /*if(inContact == false){
            stopMoving();
        }*/
        //if(Input.GetKeyDown(KeyCode.Space)){
        if(inContact == true){
            startMoving();
        }

    }
}
