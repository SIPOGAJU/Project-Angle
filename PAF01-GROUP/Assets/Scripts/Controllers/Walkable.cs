using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkable : MonoBehaviour
{
    public List<WalkPath> possiblePaths = new List<WalkPath>();

    [Space]
    [HideInInspector]
    public Transform previousBlock;

    [Space]

    [Header("Booleans")]
    public bool isStair = false;
    public bool movingGround = false;
    public bool isGoalCube = false;

    [Space]

    [Header("Offsets")]
    public float walkPointOffset = .5f;
    public float stairOffset = .4f;

    [Space]
    public bool hasPushableOnTop;
    private void Update()
    {
        RayCastUp();
    }

    public Vector3 GetWalkPoint()
    {
        float stair = isStair ? stairOffset : 0;
        return transform.position + transform.up * walkPointOffset - transform.up * stair;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        float stair = isStair ? .4f : 0;
        Gizmos.DrawSphere(GetWalkPoint(), .1f);

        if (possiblePaths == null)
            return;

        foreach (WalkPath p in possiblePaths)
        {
            if (p.target == null)
                return;
            Gizmos.color = p.active ? Color.black : Color.clear;
            Gizmos.DrawLine(GetWalkPoint(), p.target.GetComponent<Walkable>().GetWalkPoint());
        }

        Gizmos.color = Color.blue;
        Ray ray = new Ray(transform.position, transform.up);
        Gizmos.DrawRay(ray);
    }
    
    private void RayCastUp()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.up, out hit))
        {
            if (hit.collider.CompareTag("Pushable"))
            {
                hasPushableOnTop = true;
                foreach (var path in possiblePaths)
                {
                    path.active = false;
                }
            }
            else if(!hit.collider.CompareTag("Pushable") && !movingGround)
            {
                hasPushableOnTop = false;
                foreach (var path in possiblePaths)
                {
                    if(path.active == true)
                        path.active = true;
                }
            }
        }
    }
}

[System.Serializable]
public class WalkPath
{
    public Transform target;
    public bool active = true;
}
