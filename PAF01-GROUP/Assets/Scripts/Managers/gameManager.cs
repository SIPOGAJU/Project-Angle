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
        
        //player = FindObjectOfType<TouchPlayerController>(); 
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
        GameState = GAME_STATE.gamePaused; 
        CalculateScore(); 
        //Disable Input
        //Show EndgameScreen with Collectibles and Score
    }

    public void CalculateScore()
    {
        float baseScore = 1000; 
        baseScore *= currentCollectibles; 
        //If more clicks are used then optimalClickAmount, points are taken away; One Click equals 200points; 
        float clicks = GUIManager.instance.optimalClickAmount - GUIManager.instance.clickAmount;  
        if(clicks < 0 )
        {
            baseScore -= clicks * 200f; 
        }

        score = baseScore; 

    }
}
