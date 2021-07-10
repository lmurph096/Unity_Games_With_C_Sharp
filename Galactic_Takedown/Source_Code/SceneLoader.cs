using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()                               //Adds one to the current scene index to obtain the index of the next scene following it.  
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }  

    public void LoadStartScene()                              //Subtracts the index of the current scene from itself to obtain the index of the start scene. 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - currentSceneIndex);
        FindObjectOfType<GameStatus>().ResetGame();
    }

    public void LoadLevelOne()                 //Loads level 1
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadLevelTwo()                //Loads level 2
    {
        SceneManager.LoadScene("Level 2");
    }

    public void LoadLevelThree()              //Loads level 3
    {
        SceneManager.LoadScene("Level 3");
    }

    public void LoadLevelSelect()             //Loads the level select scene
    {
        SceneManager.LoadScene("Level Select");
    }


    public void Quit()                       //Exits the application
    {
        Application.Quit();
    }
}
