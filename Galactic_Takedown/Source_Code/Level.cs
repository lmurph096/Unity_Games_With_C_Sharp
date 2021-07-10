using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks;   //Serialized to observe how many blocks are required to break in order to progress to the next level
    [SerializeField] int ballNumber;        //Serialized to observe how many balls can pass through the lose collider before the level is failed

    // cached reference
    SceneLoader sceneloader;

    private void Start()
    {
        sceneloader = FindObjectOfType<SceneLoader>();      //Access the SceneLoader class. 
        
    }

    public void CountBlocks()      //Counts the total number of breakable blocks within the current level
    {
        breakableBlocks++;
    }

    public void BlockDestroyed()   //When accessed, subtracts one from the total number of breakable blocks. If there are no breakable blocks left, the current level is completed and the next level is loaded via LoadNextScene()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneloader.LoadNextScene();
        }
    }

    public void CountBalls()       //Counts the total number of balls in play within the current level
    {
        ballNumber++;
                      
    }

    public void BallsDestroyed()     //When accessed, subtracts 1 from the current number of balls in play. If there are no balls left in play, the level is failed and the title screen is loaded through LoadStartScene()
    {
        ballNumber--;
        if (ballNumber <= 0)
        {
            sceneloader.LoadStartScene();
        }
    }
  
}
