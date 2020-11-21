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
}
