using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    
    public static gameManager instance; 


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
        if(instance == null) {
            instance = this; 
        }
        else { 
            Destroy(this.gameObject); 
        }
    }
}
