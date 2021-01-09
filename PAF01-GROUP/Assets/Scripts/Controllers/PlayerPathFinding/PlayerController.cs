using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 targetPos;
    Vector3 currentPos;
    private bool isMoving;
    private Transform currentCube;

    private void Start()
    {
        RayCastDown();
        transform.position = currentCube.GetComponent<Walkable>().GetWalkPoint() + transform.up / 2f;
    }
    private void Update()
    {
        RayCastDown();

        if (currentCube.GetComponent<Walkable>().movingGround)
        {
            transform.parent = currentCube.transform;
        }
        else
        {
            transform.parent = null;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Walkable>() != null && hit.transform.up == transform.up)
                {
                    targetPos = hit.transform.GetComponent<Walkable>().GetWalkPoint();
                    Vector3 direction = (transform.position - targetPos).normalized;
                    Debug.Log(direction);
                    if(Mathf.Abs(direction.x) > .95f || Mathf.Abs(direction.z) > .95f)
                    {
                        isMoving = true;
                    }
                }
            }
        }

        if (isMoving)
        {
            Move();
        }
    }

    private void RayCastDown()
    {
        Ray playerRay = new Ray(transform.position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentCube = playerHit.transform;
            }
        }

    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 10f * Time.deltaTime);

        if (transform.position == targetPos)
        {
            isMoving = false;
        }
    }
}