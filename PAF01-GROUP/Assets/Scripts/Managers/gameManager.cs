using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    
    public static gameManager instance; 

    [Header("GameObjects")]
    PlayerController player; 

    [Header("Variables")]

    [SerializeField] int maxCollectibles = 3; 
    public int currentCollectibles;  
    bool gotAllCollectibles; 

    public float score; 


    public enum GAME_STATE
    {
        gameRunning,
        gameOver,
        gameFinished,
        gamePaused
    }
    public GAME_STATE GameState;

    private void Awake() 
    {
        if(instance == null) 
        {
            instance = this; 
        }
        else
        { 
            Destroy(this.gameObject); 
        }
    }

    public void Start()
    {
        
        player = FindObjectOfType<PlayerController>(); 
        if(player != null)
            player.OnGameOver += GoalReached; 
        currentCollectibles = 0; 

        GameState = GAME_STATE.gameRunning; 

        
    }

    public void Update() 
    {
        if(maxCollectibles <= currentCollectibles)
        {
            gotAllCollectibles = true; 
        }
        if(gotAllCollectibles)
        {
            //Assign Score? 
        }
        
    }

    public void SetCollectibleAmount()
    {
        currentCollectibles++; 
    }

    public void PauseGame()
    {
        //Allows to switch back and forth;
        if(GameState == GAME_STATE.gamePaused )
            GameState = GAME_STATE.gameRunning; 
        else
            GameState = GAME_STATE.gamePaused; 
    }

    public void QuitApplication()
    {
        //Does this work on Mobile? 
        //Could go back to Main Menu Screen instead
        Application.Quit(); 
    }

    public void GoalReached()
    {
        GameState = GAME_STATE.gameOver; 
        if(GUIManager.instance.scoreText != null)
        {
            GUIManager.instance.GameOver(0, 1);

        }
        else 
        {
            CalculateScore(out score); 
            GUIManager.instance.GameOver(score, currentCollectibles);
        }
         
        //Debug.Log("Goal Reached + " + score); 
        //Disable Input
        //Show EndgameScreen with Collectibles and Score
    }

    public void CalculateScore(out float returnScore)
    {
        float clicks = GUIManager.instance.clickAmount; 
        float optimalClicks = GUIManager.instance.optimalClickAmount; 
        float baseScore = 1000; 
        baseScore *= currentCollectibles; 
        //If more clicks are used then optimalClickAmount, points are taken away; One Click equals 100points; 
        if(clicks > optimalClicks)
        {
            float overClicks = clicks - optimalClicks; 
            baseScore -= overClicks*100f; 
        }
        returnScore = baseScore; 
        

    }
}
