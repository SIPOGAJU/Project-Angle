using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 


public class GUIManager : MonoBehaviour
{
    public static GUIManager instance; 

    Goal goal; 

    

    
   


    [SerializeField] GameObject[] horizontalSlider = new GameObject[4]; 
    [SerializeField] GameObject[] verticalSlider = new GameObject[4];
    public float clickAmount; 
    public float optimalClickAmount; 

    [Header("UI Overlays")]

    public GameObject pauseGameOverlay; 

    [SerializeField] GameObject tutorialOverlayPageOne; 
    [SerializeField] GameObject tutorialOverlayPageTwo; 

    [Header("GameOverOverlay")]
    [SerializeField] GameObject GameOverOverlay;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject[] collectiblesUI = new GameObject[3]; 
    [SerializeField] Material collectibleUIMat; 
    [SerializeField] Material collectibleUIMatTransparent; 

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
        FindObjectOfType<PlayerController>().OnPlayerClick += SetSliderValue;
    }

    void Update()
    {
        SetSliderFill(clickAmount); 

        if(gameManager.instance.GameState == gameManager.GAME_STATE.gamePaused)
        {
            pauseGameOverlay.SetActive(true); 
        }
        else
        {
            pauseGameOverlay.SetActive(false); 

        }
    } 


    public void LoadGameFinishedGUI()
    {
        if(pauseGameOverlay == null)
            return; 
        pauseGameOverlay.SetActive(true); 
        

    }

    public void PauseGameOverlay()
    {
        if(pauseGameOverlay == null)
            return; 
        if(gameManager.instance.GameState == gameManager.GAME_STATE.gamePaused)
        {
            
            pauseGameOverlay.SetActive(true);
        }
        else 
        {
            
            pauseGameOverlay.SetActive(false); 
        }
        
    }

    public void GameOver(float score, float collectibles)
    {
        GameOverOverlay.SetActive(true); 
        float clampedScore = Mathf.Clamp(score, 0,1000000); 
        scoreText.text = clampedScore.ToString(); 

        //Set Collectibles
        Color ogColor = collectiblesUI[1].GetComponent<MeshRenderer>().material.color; 
        Color transparentColor = ogColor ; 
        transparentColor.a = 200; 
        
        
        switch(collectibles)
        {
            case 0: 
                foreach(GameObject collectibleUI in collectiblesUI)
                {
                    collectibleUI.GetComponent<MeshRenderer>().material = collectibleUIMatTransparent; 
                }
                break; 
            case 1: 
                collectiblesUI[0].GetComponent<MeshRenderer>().material = collectibleUIMat;  
                collectiblesUI[1].GetComponent<MeshRenderer>().material = collectibleUIMatTransparent;  
                collectiblesUI[2].GetComponent<MeshRenderer>().material = collectibleUIMatTransparent;  
                break;
            case 2: 
                collectiblesUI[0].GetComponent<MeshRenderer>().material = collectibleUIMat; 
                collectiblesUI[1].GetComponent<MeshRenderer>().material = collectibleUIMat; 
                collectiblesUI[2].GetComponent<MeshRenderer>().material = collectibleUIMatTransparent; 
                break;
            case 3: 
                foreach(GameObject collectibleUI in collectiblesUI)
                {
                    collectibleUI.GetComponent<MeshRenderer>().material = collectibleUIMat; 
                }
                break;

        }
        

        
    }

    public void CloseUIWindow(GameObject UIWindow)
    {
        UIWindow.SetActive(false);  
    }

    

    public  void SetSliderValue()
    {
        clickAmount++;
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

    public void SwitchTutorialPages(int page)
    {
        if(page == 1)
        {
            tutorialOverlayPageOne.SetActive(true);
            tutorialOverlayPageTwo.SetActive(false);
        }
        else if(page == 2)
        {
            tutorialOverlayPageOne.SetActive(false);
            tutorialOverlayPageTwo.SetActive(true);
        }
        else
            return;
    }


}
