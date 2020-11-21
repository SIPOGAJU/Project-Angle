using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    
    public static gameManager instance; 

    [Header("GameObjects")]
    Collectible collectible; 
    PlayerController player; 

    [Header("Variables")]

    [SerializeField] int maxCollectibles = 1; 
    int currentCollectibles;  


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

    public void Start()
    {
        collectible = FindObjectOfType<Collectible>(); 
        collectible.OnCollected += SetCollectibleAmount; 

        player = FindObjectOfType<PlayerController>(); 

        GameState = GAME_STATE.gameRunning; 
    }

    public void Update() 
    {
        
        
    }

    public void SetCollectibleAmount()
    {
        currentCollectibles++; 
        if(maxCollectibles <= currentCollectibles)
        {
            Debug.Log("All Collectibles have been collected"); 
        }
            
    }
}
