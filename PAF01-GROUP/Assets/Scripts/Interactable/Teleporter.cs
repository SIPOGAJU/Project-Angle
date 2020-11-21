using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform exit;
    public PlayerController player;

    private bool isOverlapping;

    private void Update()
    {
        if(player != null)
        {
            if ((Input.GetKeyDown(KeyCode.E)) && isOverlapping == true)
            {
                player.transform.position = exit.position;
                player._targetPos = exit.position;
                isOverlapping = false;
            }
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            isOverlapping = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
            isOverlapping = false;
    }
}
