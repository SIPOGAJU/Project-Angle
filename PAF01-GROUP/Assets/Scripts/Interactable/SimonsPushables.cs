using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonsPushables : MonoBehaviour
{
    Collider objectCollider; 
    GameObject player; 


    public Vector3 moveDirection; 
    public float moveLength; 

    public bool canMove; 


    ////////////////// PUSHABLES ///////////////////////////
    // 
    // After Trigger Enter, check Direction of Player
    // Create direction Vector with only one value e.g. (0,0,1)
    // Raycast checks for Obstacle behind Pushable
    // If no obstacle -> Move the pushable and the player;
    // If obstacle -> Stop; 
    //
    // TO-DO:
    // - Only Move the Player after the Pushable is moved; 
    // -> Raycast hit from camera to mouse position; Check for Pushable Object; 
    // -> If yes, execute this script, before you move the player; 
    //
    // - Pushable should not move outside of tiles; 
    //
    //////////////////////////////////////////////////////////

    void Start()
    {
        //Needs to be ignored by own raycaast
        objectCollider = GetComponent<Collider>(); 
        player = GameObject.FindGameObjectWithTag("Player"); 

        
        
    }

    private void OnTriggerEnter(Collider c)
    {
        if(c.CompareTag("Player"))
        {
            PlayerDirectionCheck(); 
        }
    }


    public void PlayerDirectionCheck()
    {
        //Reset Vectors
        moveDirection = Vector3.zero; 
        Vector3 distance = Vector3.zero; 
        Vector3 moveDirectionNormalized = Vector3.zero; 

        //Vector between player and pushable
        distance = transform.position - player.transform.position;
        moveDirectionNormalized = distance.normalized;
        
        //Vector should only have one value like (0,0,1)
        //X
        if(moveDirectionNormalized.x > 0.8f )
        {
            moveDirection.x = 1; 
        }
        else if(moveDirectionNormalized.x < - 0.8f )
        {
            moveDirection.x = -1; 

        }
        else
        {
            moveDirectionNormalized.x = 0; 
        }

        //Y
        if(moveDirectionNormalized.y > 0.8f )
        {
            moveDirection.y = 1; 
        }
        else if(moveDirectionNormalized.y < - 0.8f )
        {
            moveDirection.y = -1; 

        }
        else
        {
            moveDirection.y = 0; 
        }
        
        //Z
        if(moveDirectionNormalized.z > 0.8f )
        {
            moveDirection.z = 1; 
        }
        else if(moveDirectionNormalized.z < - 0.8f )
        {
            moveDirection.z = -1; 

        }
        else
        {
            moveDirection.z = 0; 
        }
        
        
        //Debug.Log(moveDirection);

        CheckForObstacle(); 
        
    }
     public void CheckForObstacle()
    {
        
        RaycastHit hit;
        
        if(Physics.Raycast(transform.position, moveDirection, out hit, 3))
        {
            canMove = false;
            Debug.Log("Do Not Move Player!"); 

        }
        if(!Physics.Raycast(transform.position, moveDirection, out hit, 3))
        {
            canMove = true; 
            Debug.Log("Can Move"); 
            Move(); 
        }

    }

    public void Move()
    {
        Vector3 targetPosition = Vector3.zero; 
        targetPosition = transform.position + moveDirection*moveLength; 

        transform.position = targetPosition; 
    }


}

