using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTeleporter : MonoBehaviour
{
    public Transform exit1;
    public Transform exit2;
    public TouchPlayerController player;

    public bool wasVisited;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(wasVisited == false)
            {
                player.transform.position = exit1.position;
                player.transform.up = exit1.transform.up;
                player._targetPos = exit1.position;
                wasVisited = true;
            }

            else if(wasVisited == true && exit2 != null)
            {
                player.transform.position = exit2.position;
                player.transform.up = exit2.transform.up;
                player._targetPos = exit2.position;
                wasVisited = false;
            }
        }
    }
}
