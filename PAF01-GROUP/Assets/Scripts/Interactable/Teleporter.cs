using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Teleporter : MonoBehaviour
{
    public Transform exit1;
    public Transform exit2;

    public bool wasVisited;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(wasVisited == false)
            {

                DOTween.KillAll(other.gameObject);
                other.gameObject.transform.position = exit1.position;
                other.gameObject.transform.up = exit1.transform.up;
                wasVisited = true;

                FindObjectOfType<AudioManager>().Play("CamTrans");
            }

            else if(wasVisited == true && exit2 != null)
            {

                DOTween.KillAll(other.gameObject);
                other.gameObject.transform.position = exit2.position;
                other.gameObject.transform.up = exit2.transform.up;
                wasVisited = false;
            }
        }
    }
}
