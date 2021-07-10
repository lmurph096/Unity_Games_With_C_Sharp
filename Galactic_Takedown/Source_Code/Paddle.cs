using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
    
{
    [SerializeField] float screenWidthInUnits = 16f;    //Defines the total screen width in Units 
    [SerializeField] float xMin = 1f;                   //The limit at which the paddle can travel towards the left side of the screen
    [SerializeField] float xMax = 15f;                  //The limit at which the paddle can travel towards the right side of the screen 

    // cached references
    GameStatus theGameStatus;
    Ball theBall;

    // Start is called before the first frame update
    void Start()
    {
        theGameStatus = FindObjectOfType<GameStatus>();      //Access the GameStatus class
        theBall = FindObjectOfType<Ball>();                  //Access the Ball class. 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);      //During each frame, access the x and y positions of the paddle 
        paddlePos.x = Mathf.Clamp(GetXPos(), xMin, xMax);                                 //Obtains the current x position of the paddle via GetXPos(), then restricts it to within a range defined by xMin and xMax
        transform.position = paddlePos;                                                   //Updates the x and y coordinates of the paddle (transform position) according to any changes in GetXPos()
    }

    private float GetXPos()
    {
        if (theGameStatus.IsAutoPlayEnabled())               //If the game is in auto-play mode, get the current x coordinates of the paddle 
        {
            return theBall.transform.position.x;
        }
        else                                                //If the game is not in auto-play mode, get current x coordinates of the paddle
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;   //Input.mousePosition.x returns the x position of the paddle, in terms of the number of pixels present over the x axis of the screen (this depends on screen resolution)
        }                                                                       //Dividing Input.mousePosition.x by Screen.width will return x values between 0 (far left of the screen), and 1 (far right of the screen)
    }                                                                           //Multiplying by screenWidthInUnits will define the x position of the paddle in terms of Unity Units. In this case between 0 (far left of screen), and 16 (far right of screen)

}
