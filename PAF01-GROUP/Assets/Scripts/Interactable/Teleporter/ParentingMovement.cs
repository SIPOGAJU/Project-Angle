using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentingMovement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = transform;
            other.gameObject.transform.up = transform.up;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent = null;
        }
    }
}
