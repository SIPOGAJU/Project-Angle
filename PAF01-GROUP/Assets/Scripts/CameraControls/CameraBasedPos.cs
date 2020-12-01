using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBasedPos : MonoBehaviour
{
    public Transform cam;
    public float angleToTrack;
    public Transform[] wayPoints;
    public float speed;
    int currentWayPoint;

    public bool xRotation;
    public bool zRotation;

    private void Start()
    {
        currentWayPoint = 0;
    }
    void Update()
    {
        if(xRotation == true)
        {
            if (Mathf.Abs(cam.transform.rotation.eulerAngles.x - angleToTrack) <= 2f)
            {
                currentWayPoint = 1;
            }
        }

        if(zRotation == true)
        {
            if (Mathf.Abs(cam.transform.rotation.eulerAngles.z - angleToTrack) <= 2f)
            {
                currentWayPoint = 1;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPoint].position, speed * Time.deltaTime);
    }
}
