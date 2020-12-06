using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    bool mainMenu; 

    public GameObject LevelSelectionItems; 
    public GameObject MainMenuObjects; 
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = true; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelSelectionScreen()
    {
         

        if(mainMenu)
        {
            MainMenuObjects.gameObject.SetActive(false); 
            LevelSelectionItems.gameObject.SetActive(true); 
            mainMenu = false; 
        }
        else
        {
            MainMenuObjects.gameObject.SetActive(true); 
            LevelSelectionItems.gameObject.SetActive(false);
            mainMenu = true; 
        }
    }
}
