using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    Player player; 

    void Start()
    {
        player = FindObjectOfType<Player>(); 
        player.OnPlayerDeath += GameOver; 
    }

    void Update()
    {
        
    }


    public void GameOver()
    {
        
    }
}
