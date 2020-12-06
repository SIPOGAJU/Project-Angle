using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


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
        if(gameFininishedObjects == null)
            return; 
        gameFininishedObjects.SetActive(true); 
        

    }

    public void PauseGameOverlay()
    {
        if(gameFininishedObjects == null)
            return; 
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
        //Method needs to include all 3 CollectibleCounters in order to work; 
        if(collectibleCounter1 == null || collectibleCounter2 == null || collectibleCounter3 == null)
            return; 
        int myCurrentCollectibles =  gameManager.instance.currentCollectibles; 

        
        if(myCurrentCollectibles == 1)
        { 
            collectibleCounter1.GetComponent<Image>().color = new Color32(255,250,77,255);
        }
        if(myCurrentCollectibles == 2)
        {
            collectibleCounter2.GetComponent<Image>().color = new Color32(255,250,77,255); 
        }
        if(myCurrentCollectibles == 3)
        {
            collectibleCounter3.GetComponent<Image>().color = new Color32(255,250,77,255); 
        }
    }
}
