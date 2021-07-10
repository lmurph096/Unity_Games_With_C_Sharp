using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();    //Access the Level class
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        level.BallsDestroyed();               //When the ball passes through the Lose Collider, run the BallsDestroyed() method within the Level class. 
    }
       
}
