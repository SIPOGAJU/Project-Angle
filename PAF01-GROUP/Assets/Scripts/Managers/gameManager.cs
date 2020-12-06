using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    
    public static gameManager instance; 

    [Header("GameObjects")]
    PlayerController player; 

    [Header("Variables")]

    [SerializeField] int maxCollectibles = 1; 
    public int currentCollectibles;  
    bool gotAllCollectibles; 


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
        GUIManager.instance.SetCollectibleGUI(); 

        
            
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
}
