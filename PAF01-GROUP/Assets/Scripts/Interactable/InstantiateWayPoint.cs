using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateWayPoint : MonoBehaviour
{
    public GameObject wayPrefab;
    public bool isWalkable;

    // Start is called before the first frame update
    void Start()
    {
        if(isWalkable == true)
        {
            Instantiate(wayPrefab, (transform.position + transform.up * .5f), transform.rotation, transform);
        }
    }
}
