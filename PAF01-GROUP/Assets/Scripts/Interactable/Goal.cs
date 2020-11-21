using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class Goal : MonoBehaviour
{
    public event Action GoalCollected; 

    private void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            
            if(GoalCollected != null)
                GoalCollected(); 
                //Gets Called in GUI Manager
                //Should also disable PlayerMovement

            Destroy(this.gameObject); 
        }
    }
}
