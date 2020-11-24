using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GUIManager : MonoBehaviour
{
    public static GUIManager instance; 


    Goal goal; 

    [SerializeField] GameObject gameFininishedObjects; 
    [SerializeField] GameObject wordlTurningButtons; 
    [SerializeField] GameObject collectibleCounter1; 
    [SerializeField] GameObject collectibleCounter2; 
    [SerializeField] GameObject collectibleCounter3; 

    public int GUIcurrentCollectibles = 0;  

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
        wordlTurningButtons.SetActive(false); 

    }

    public void PauseGameOverlay()
    {
        if(gameManager.instance.GameState == gameManager.GAME_STATE.gamePaused)
        {
            wordlTurningButtons.SetActive(true); 
            gameFininishedObjects.SetActive(false);
        }
        else 
        {
            wordlTurningButtons.SetActive(false); 
            gameFininishedObjects.SetActive(true); 
        }
        
    }

    public void SetCollectibleGUI()
    {
        int currentCollectibles = gameManager.instance.currentCollectibles; 

        
        if(currentCollectibles == 1)
        {
            collectibleCounter1.SetActive(true); 
        }
        if(currentCollectibles == 2)
        {
            collectibleCounter2.SetActive(true);
        }
        if(currentCollectibles == 3)
        {
            collectibleCounter3.SetActive(true);
        }
    }
}
