using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GUIManager : MonoBehaviour
{
    public static GUIManager instance; 

    Goal goal; 

    [SerializeField] GameObject gameFininishedObjects; 

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


    public void LoadGameFinishedGUI()
    {
        gameFininishedObjects.SetActive(true); 
    }
}
