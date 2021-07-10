using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    // config parameters
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;    //Serialized to allow for the increase or decrease of the game speed, with the range of 0.1 (slow) and 10 (fast)
    [SerializeField] int pointsPerBlockDestroyed = 100;          //Determines how many points the player recieves for destroying a block
    [SerializeField] TextMeshProUGUI scoreText;                  //Allows the user to choose the UI element which displays the current score   
    [SerializeField] bool isAutoPlayEnabled;                     //Allows the user to enable auto-play mode 

    // state variables
    [SerializeField] int currentScore = 0;                       //Initialisation of the score at the start of the level

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;   //Stores the current number of GameStatus objects
        if (gameStatusCount > 1)                                         
        {                                                       //If more than one GameStatus object is present, any new GameStatus objects that are created will be destroyed. 
            Destroy(gameObject);
        }

        else                                                    //If only one GameStatus object is present, allow it to persist when a new level is loaded. 
        {
            DontDestroyOnLoad(gameObject);                     //Essentially, this process allows us to ensure that the player score is not reset when a new level is loaded. 
        }                                                      //Within Unity engine, if our Score Text is a child of GameStatus, then both GameStatus and Score Text will persist when a new scene is loaded. 
    }                                                          

    private void Start()
    {
        scoreText.text = currentScore.ToString();     //Takes the currentScore and converts it to a String format. This String is displayed on the text based UI element associated with the scoreText variable. 

    }



  

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;       //Each frame, specify the game speed. Where 1f is standard game speed.  
    }

    public void AddToScore()        //When called, this method will add points to the total player score. The updated score is displayed on the UI text element associated with scoreText. 
    {
        currentScore = currentScore + pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()        //Destory the score text when the game is reset. 
    {
        Destroy(gameObject);       

    }

    public bool IsAutoPlayEnabled()      //Returns a true/false value depending on whether the user has enabled autoplay in Unity.
    {
        return isAutoPlayEnabled;
    }

}
