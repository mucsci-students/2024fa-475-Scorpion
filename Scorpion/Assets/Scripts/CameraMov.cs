using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    public bool moving = false;     //To be used later for stop and go implementation
    public float camSpeed = 1f;
    public float boundHeight = 200f;
    public CamTrigger startOne;
    public CamTrigger startTwo;
    //public CamTrigger startThree;
    public CamTrigger stopOne;
    //public CamTrigger stopTwo;
    //public CamTrigger stopThree;
    private bool startContact = false;
    private bool stopContact = false;
    
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
        if (startOne.moveCam() || startTwo.moveCam() /*|| startThree.moveCam()*/){
        startContact = true;
        stopContact = false;
        }
        
        if(startContact == true){
            startMoving();
        }

        if (stopOne.moveCam() /*|| stopTwo.moveCam() || stopThree.moveCam()*/){
        stopContact = true;
        startContact = false;
        }

        if (stopContact == true){
            stopMoving();
        }

        if(moving == true){
        Vector3 newPostion = transform.position + Vector3.up * camSpeed * Time.deltaTime;
        if (newPostion.y <= boundHeight && moving){
            transform.position = newPostion;
        }
        }
        
        //stopContact = false;
        //startContact = false;

    }
}
