using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Teleporter : MonoBehaviour
{
    public Transform exitA;
    public Transform exitB;
    private bool wasVisited;

    private PlayerController controller;

    private void Start()
    {
        if (FindObjectOfType<PlayerController>() != null)
            controller = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //if (FindObjectOfType<AudioManager>() != null)
            //    FindObjectOfType<AudioManager>().Play("CamTransition");

            if (wasVisited == false)
            {
                controller.StopCoroutine(controller.currentRoutine);
                controller.Clear();
                other.gameObject.transform.up = exitA.transform.up;
                other.gameObject.transform.position = exitA.GetComponent<Walkable>().GetWalkPoint() + controller.transform.up / 2f;
                wasVisited = true;

                
            }

            else if(wasVisited == true && exitB != null)
            {
                controller.StopCoroutine(controller.currentRoutine);
                controller.Clear();
                other.gameObject.transform.up = exitB.transform.up;
                other.gameObject.transform.position = exitB.GetComponent<Walkable>().GetWalkPoint() + controller.transform.up / 2f;

                wasVisited = false;
            }
        }
    }
}
