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


    private void Start()
    {
        currentWayPoint = 0;
    }
    void Update()
    {
        if (Mathf.Abs(cam.transform.rotation.eulerAngles.x - 90) <= 1f)
        {
            currentWayPoint = 1;
        }

        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWayPoint].position, speed * Time.deltaTime);
    }
}
