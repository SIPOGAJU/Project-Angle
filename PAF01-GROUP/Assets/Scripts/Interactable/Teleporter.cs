using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform exit1;
    public Transform exit2;
    public PlayerController player;

    private bool isOverlapping;
    public bool wasVisited;

    private void Update()
    {
        //if(player != null)
        //{
        //    if ((Input.GetKeyDown(KeyCode.E)) && isOverlapping == true)
        //    {
        //        player.transform.position = exit1.position;
        //        player._targetPos = exit1.position;
        //        isOverlapping = false;
        //    }
        //}   
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(wasVisited == false)
            {
                player.transform.position = exit1.position;
                player._targetPos = exit1.position;
                wasVisited = true;
                StartCoroutine(SettingTrigger());
            }

            else if(wasVisited == true && exit2 != null)
            {
                player.transform.position = exit2.position;
                player._targetPos = exit2.position;
                wasVisited = false;
                StartCoroutine(SettingTrigger());
            }
        }
           // isOverlapping = true;
    }

    private void OnTriggerExit(Collider collision)
    {
        //if (collision.gameObject.tag == "Player")
        //    isOverlapping = false;
    }

    IEnumerator SettingTrigger()
    {
        this.gameObject.GetComponent<MeshCollider>().isTrigger = false;
        yield return new WaitForSeconds(2);
        this.gameObject.GetComponent<MeshCollider>().isTrigger = true;
    }
}
