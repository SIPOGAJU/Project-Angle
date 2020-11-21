using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    [SerializeField] float unitsToMove = 0f;
    private bool isColliding;
    private bool isMoving;

    [SerializeField] Vector3 offset = Vector3.zero;
    private PlayerController player;
    Vector3 reposition;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {

        if(isColliding == true)
        {
            isMoving = true;
        }

        if(isMoving == true)
        {
            Move();
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Debug.Log("Player is pushing");
            isColliding = true;

            Vector3 target = player._targetPos + offset;
            Vector3 playerPos = player.transform.position;
            Vector3 direction = (target - playerPos).normalized;
            reposition = transform.position + direction * unitsToMove;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player is pushing");
            isColliding = false;
        }
    }

    private void Move()
    {
        
        transform.position = Vector3.Lerp(transform.position, reposition, Time.deltaTime);
        if (Vector3.Distance(transform.position, reposition) < 0.2)
        {
            isMoving = false;
        }
    }
}
