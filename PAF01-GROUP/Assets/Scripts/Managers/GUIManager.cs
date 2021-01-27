using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 


public class GUIManager : MonoBehaviour
{
    public static GUIManager instance; 

    Goal goal; 

    

    public GameObject gameFininishedObjects; 
   


    [SerializeField] GameObject[] horizontalSlider = new GameObject[4]; 
    [SerializeField] GameObject[] verticalSlider = new GameObject[4];
    public float clickAmount; 
    public float optimalClickAmount; 

    [SerializeField] GameObject tutorialOverlayPageOne; 
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
        if(goal != null)
        {
            goal.GoalCollected += LoadGameFinishedGUI;
        }

        FindObjectOfType<PlayerController>().OnPlayerClick += SetSliderValue;
    }

    void Update()
    {
        SetSliderFill(clickAmount); 
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

    public void CloseUIWindow(GameObject UIWindow)
    {
        UIWindow.SetActive(false);  
    }

    

    public  void SetSliderValue()
    {
        clickAmount++;
        if (clickAmount >= optimalClickAmount)
        {
            FindObjectOfType<PlayerController>().OnPlayerClick -= SetSliderValue;
        }
            
    }

    public void SetSliderFill(float clicks)
    {
        if(clicks < optimalClickAmount/2)
        {
            
            foreach(GameObject slider in verticalSlider)
            {
                //Value lies between 0.51 and 1; 
                float value = Mathf.Lerp(slider.GetComponent<Slider>().value, (1-(clicks / optimalClickAmount)), 0.01f); 
                slider.GetComponent<Slider>().value = value; 
            }
        }
        else if(clicks > optimalClickAmount / 2)
        {
            //Sets Vertical Sliders to 0; 
            foreach(GameObject slider in verticalSlider)
            {
                slider.GetComponent<Slider>().value = .5f; 
            }

            foreach(GameObject slider in horizontalSlider)
            {
                float value = Mathf.Lerp(slider.GetComponent<Slider>().value, (1-(clicks / optimalClickAmount)), 0.01f); 
                slider.GetComponent<Slider>().value = value;
            }
        }
    }


}
