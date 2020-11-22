using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Collectible : MonoBehaviour
{
    public event Action OnCollected; 

    private void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            
            if(OnCollected != null)
                OnCollected(); 
                //Gets Called in gameManager

            Destroy(this.gameObject); 
        }
    }

}
