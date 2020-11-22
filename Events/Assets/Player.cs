using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Player : MonoBehaviour
{
    public float health = 10; 
    public event Action OnPlayerDeath; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            OnPlayerDeath(); 
            Destroy(gameObject ); 
        }
    }
}
