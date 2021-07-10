using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // configuration parameters
    [SerializeField] AudioClip breakSound;             //Serialized to allow the user to assign the desired audio files in Unity.
    [SerializeField] GameObject blockSparklesVFX;      //Serialized to allow the user to select the desired particle effects in Unity.
    [SerializeField] Sprite[] hitSprites;              //Serialized to allow the user to select the desired block sprites in Unity. 

    // Cached reference
    Level level;

    //state variables 
    int timesHit;

    
    private void Start()      //At the start of each level, count all blocks with the 'breakable' tag. 
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();    //Access the level class

        if (tag == "Breakable")               //For each block with a 'breakable' tag in play, run the CountBlocks() method which adds 1 to the breakableBlocks variable. 
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)    //When a ball collides with a block containing the 'Breakable' or 'BreakableNull' tag, run the HandleHit() method. 
    {
        if (tag == "Breakable" || tag == "BreakableNull")       
        {
            HandleHit();
        }
    }

    private void ShowNextHitSprite()              //'hitSprites' is an array containing various block sprites with differing degrees of damage. A block which hasn't been hit, has the default undamaged sprite.
    {                                             //A sprite which has been hit once will display a slightly damaged sprite, corresponding to the 0th element of the array etc. 
        int spriteIndex = timesHit - 1;           
        if (hitSprites[spriteIndex] != null)        //If a certain element of the 'hitSprites' array determined by 'spriteIndex' is not empty, render the block sprite corresponding to that element.  
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else                                                               //Used for debugging purposes, to ensure that the 'hitSprites' array for all blocks is filled.  
        {
            Debug.LogError("Block sprite " + gameObject.name + " is missing from array");
        }
    }

    private void HandleHit()           //When called, this method adds 1 to the 'timesHit' variable. If the block is hit more times than the length of the 'hitSprites' array, then the block is destroyed.
    {                                  //If the block is hit fewer times than the length of the 'hitSprites' array, then the next block sprite is loaded (sprites with increasing levels of damage are displayed).
        timesHit++;                            
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void DestroyBlock()       //This method destroys a block when accessed.  
    {

        PlayBlockDestroySFX();              //Play a specific sound effect when the block is destroyed.
        gameObject.SetActive(false);        //Destroy the block        

        if (tag == "Breakable")          //If the block has a 'Breakable' tag, subtract 1 from the total number of breakable blocks. When no breakable blocks remain, the next level is loaded. 
        {                                //This does not apply to block with the tag 'BreakableNull', these blocks are breakable, but do not contribute to level progression. The player does not need to destroy all 'BreakableNull' blocks to progress.
            level.BlockDestroyed();
        }
        TriggerSparklesVFX();            //Trigger a particle effect when the block is destroyed. 

    }

    private void PlayBlockDestroySFX()
    {
        FindObjectOfType<GameStatus>().AddToScore();                                        //When accessed, adds a certain amount of points to the player's total score, determined by the 'pointsPerBlockDestroyed' variable.
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, 0.3f);     //'breakSound' is an array containing different sound effects. This will trigger a sound effect from this array, at the location of the Main Camera.
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);    //Triggers particle effects at the xyz coordinates of the block.  
        Destroy(sparkles, 1f);                                                                         //Destroy the particle effect after 1 second. 
    }

}
