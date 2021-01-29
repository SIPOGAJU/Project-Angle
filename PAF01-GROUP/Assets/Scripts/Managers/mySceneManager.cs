using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class mySceneManager : MonoBehaviour
{
    public static mySceneManager instance;
    //mySceneManager is name of Script and SceneManager is Class from Unity; 



    

    void Awake()
    {
        if(instance == null) { 
            instance = this; 
        }
        else { 
            Destroy(this.gameObject); 
        }


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.GameState = gameManager.GAME_STATE.gameRunning;
        
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameManager.instance.GameState = gameManager.GAME_STATE.gameRunning;
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index); 
        gameManager.instance.GameState = gameManager.GAME_STATE.gameRunning;

    }

    public void LoadPreviousScene()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
            return; 

        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
        gameManager.instance.GameState = gameManager.GAME_STATE.gameRunning;
    }

    

   

    

}
