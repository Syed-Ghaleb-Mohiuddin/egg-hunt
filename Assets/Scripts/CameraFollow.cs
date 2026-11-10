using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private  Transform playerPos;
    public BirdMovement trigger;
    public int dist = 6;
    public float speed = 0.1f;
    public GameObject Bird;

    void Start()
    {

        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        trigger  = GameObject.FindGameObjectWithTag("Player").GetComponent<BirdMovement>();
        Bird = GameObject.Find("frame-1");
       
    }
    bool once = true;
    void Update()
    {
        /*if (Bird.activeSelf==true)
        {
            trigger = GameObject.FindGameObjectWithTag("Player").GetComponent<BirdMovement>();
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        }*/
      
    }
    // Update is called once per frame
    void LateUpdate()
    {
        
        if (trigger != null)
        {
        trigger = GameObject.FindGameObjectWithTag("Player").GetComponent<BirdMovement>();
            playerPos = GameObject.FindGameObjectWithTag("Player").transform;
            trigger = GameObject.FindGameObjectWithTag("Player").GetComponent<BirdMovement>();
            Vector3 tempPos = transform.position;
            tempPos.x = playerPos.position.x * speed + dist;
        transform.position = tempPos;
            once = true;
        }    
    }
    void FollowAfterDestroy()
    {
       

            transform.position = new Vector3(0.21f, 0, -10) ;
        
    }
    

}
