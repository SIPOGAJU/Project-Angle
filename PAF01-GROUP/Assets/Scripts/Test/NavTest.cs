using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    private Vector3 _targetPos;
    public NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetDestination();
        }
    }
    void SetTargetDestination()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            _targetPos = hit.point;
            navAgent.SetDestination(_targetPos);

            Vector3 direction = navAgent.destination - navAgent.transform.position;
            direction.y = 0f;

            //if absolute unsigned value of 'x' is higher that unsigned value of 'z'
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
                //then we know that nav agent should turn straight to the right (on X axis)
                //so, set the forward axis to '0'
                direction.z = 0f;
            else
                //otherwise make him turn straight to Z axis (set the right axis to '0')
                direction.x = 0f;
            //we've set 'direction.y' to '0' earlier so we could rotate transform to the direction
            //and it will be rotated around its Y axis only.
            navAgent.transform.rotation = Quaternion.LookRotation(direction);

        }
    }
}
