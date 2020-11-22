using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] Vector3 doorTargetPos; 
    Vector3 doorStartPosition; 
    [SerializeField] GameObject door; 

    bool doorIsOpen = false; 

    private void Start()
    {
        doorStartPosition = door.transform.position; 
    }


    private void OnTriggerEnter(Collider c) 
    {
        if(c.CompareTag("Player"))
        {
            if(doorIsOpen == false)
            {
                door.transform.position = doorStartPosition + doorTargetPos; 
                doorIsOpen = true; 
            }
            else if(doorIsOpen == true)
            {
                door.transform.position = doorStartPosition; 
                doorIsOpen = false; 
            }
            

        }

    }


}
