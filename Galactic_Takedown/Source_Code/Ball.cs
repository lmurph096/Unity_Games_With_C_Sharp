using UnityEngine;

public class Ball : MonoBehaviour
    
{
    // config parameters
    [SerializeField] Paddle paddle1;                //Serialized to allow the user to select the desired paddle sprite in Unity. 
    [SerializeField] float xPush = 2f;              //Variable to modify the ball trajectory in the x direction when launched from the paddle.
    [SerializeField] float yPush = 15f;             //Variable to modify the ball trajectory in the y direction when launched from the paddle.  
    [SerializeField] AudioClip[] ballSounds;        //Serialized to allow the user to choose the desired audio file for the ball collision in Unity
    [SerializeField] float randomFactor = 0.2f;     //A variable which slighly modifies the trajectory of the ball in the y direction upon collision with a game object.  

    //state 
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    Level level;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;     //Subtracts the coordinates of the ball from the coordinates of the paddle. The resulting vector is at the interface between the ball and paddle. 
        myAudioSource = GetComponent<AudioSource>();                             //Access AudioSource
        myRigidBody2D = GetComponent<Rigidbody2D>();                             //Access RigidBody2D
        CountBallsInPlay();                                                      //A method which adds 1 to the 'ballNumber' variable for each ball in play. 

    }

    // Update is called once per frame
    void Update()

    {        
        if (hasStarted != true)           //At the start of the level 'hasStarted' is false. 
        {
            LockBallToPaddle();          //The x position of the ball mirrors the x position of the paddle. 
            LaunchOnMouseClick();        //Launches the ball away from the paddle once the right mouse button is clicked. 
        }        
    }

    private void CountBallsInPlay()     //This method counts all balls within the current level.
    {
        level = FindObjectOfType<Level>();    //Access the Level class.

        if (tag == "Ball")                    //If a game object has the 'Ball' tag, add 1 to the 'ballNumber' variable. 
        {
            level.CountBalls();
        }

    }

        


    private void LockBallToPaddle()     //This method ensures that the movement of the ball on the x axis, mirrors the movement of the paddle. 
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);    //A vector which contains the x and y positions of the paddle.
        transform.position = paddlePos + paddleToBallVector;                                           //The position of the ball is the position of the paddle, shifted by 'paddleToBallVector'.
    }
    
    private void LaunchOnMouseClick()          //If the player clicks the right mouse button, the 'hasStarted' variable is change to true, meaning the contents of the Update() method no longer run. 
    {                                          //Then the ball is launched away from the paddle, with a velocity in the x and y directions determined by xPush and yPush respectively. 
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);            
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)     //When the ball collides wih a surface, the trajectory of the ball is tweaked slightly. This is to prevent the ball from bouncing endlessly along the x axis. 
    {
        Vector2 velocityTweak = new Vector2                   //A vector which tweaks the trajectory of the ball somewhere within the range of 0 to 'randomFactor' in the x and y directions. 
            (Random.Range(0f, randomFactor),
            (Random.Range(0f, randomFactor)));


        if (hasStarted)                                       //'hasStarted' == true, when the player has clicked the right mouse button to launch the ball, via the LaunchOnMouseClick() method. 
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];  //'ballSounds' is an array containing various ball bounce sound effects. This picks a sound effect at random from the array.  
            myAudioSource.PlayOneShot(clip);                                              //Play the selected sound effect.

            myRigidBody2D.velocity = myRigidBody2D.velocity.normalized * yPush;            //Returns the velocity of the ball to its initial value (yPush). Without this, the ball can end up with a negative velocity
                                                                                           //due to the fact that a small amount of velocity is added/subtracted with each collision. Essentially, this resets the velocity of the ball when it collides with a new object. 

            myRigidBody2D.velocity += velocityTweak;                                     //Tweak the velocity of the ball. 
        }
        
    }
}
