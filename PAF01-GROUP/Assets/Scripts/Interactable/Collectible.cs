using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Collectible : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            gameManager.instance.SetCollectibleAmount(); 

            Destroy(this.gameObject); 
        }
    }

}
