using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GUIManager : MonoBehaviour
{
    public static GUIManager instance; 

    Goal goal; 

    public GameObject gameFininishedObjects; 
    public GameObject collectibleCounter1; 
    public GameObject collectibleCounter2; 
    public GameObject collectibleCounter3; 

    void Awake()
    {
        if(instance == null) { 
            instance = this; 
        }
        else { 
            Destroy(this.gameObject); 
        }
    }

    void Start()
    {
        goal = FindObjectOfType<Goal>(); 
        goal.GoalCollected += LoadGameFinishedGUI; 
    }

    void Update()
    {
        
    } 


    public void LoadGameFinishedGUI()
    {
        gameFininishedObjects.SetActive(true); 
        

    }

    public void PauseGameOverlay()
    {
        if(gameManager.instance.GameState == gameManager.GAME_STATE.gamePaused)
        {
            
            gameFininishedObjects.SetActive(true);
        }
        else 
        {
            
            gameFininishedObjects.SetActive(false); 
        }
        
    }

    public void SetCollectibleGUI()
    {
        int myCurrentCollectibles =  gameManager.instance.currentCollectibles; 

        
        if(myCurrentCollectibles == 1)
        {
            collectibleCounter1.SetActive(true); 
        }
        if(myCurrentCollectibles == 2)
        {
            collectibleCounter2.SetActive(true);
        }
        if(myCurrentCollectibles == 3)
        {
            collectibleCounter3.SetActive(true);
        }
    }
}
