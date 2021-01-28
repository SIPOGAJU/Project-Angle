using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    public Transform[] waypoints;
    CameraPivot camUp;
    int currentWaypoint;
    public float moveSpeed;

    public bool moveWithCamera;
    public bool moveWithPlayer;

    // Start is called before the first frame update
    void Start()
    {
        camUp = FindObjectOfType<CameraPivot>();
        currentWaypoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveWithCamera)
        {

            if (Approximately(transform.up, camUp.transform.up, 0.02f))
            {
                currentWaypoint = 1;
            }
            else
            {
                currentWaypoint = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && moveWithPlayer)
        {
            StopCoroutine(MoveCubePlayer());
            StartCoroutine(MoveCubePlayer());
        }
    }
    public bool Approximately(Vector3 me, Vector3 other, float allowedDifference)
    {
        var dx = me.x - other.x;
        if (Mathf.Abs(dx) > allowedDifference)
            return false;

        var dy = me.y - other.y;
        if (Mathf.Abs(dy) > allowedDifference)
            return false;

        var dz = me.z - other.z;
        if (Mathf.Abs(dz) > allowedDifference)
            return false;
        else 
            return true;
    }

    IEnumerator MoveCubePlayer()
    {
        yield return new WaitForSeconds(1f);
        if (currentWaypoint == 1)
            currentWaypoint = 0;
        else if (currentWaypoint == 0)
            currentWaypoint = 1;
    }
}
