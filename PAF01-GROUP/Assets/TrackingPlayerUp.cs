using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingPlayerUp : MonoBehaviour
{
    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        transform.up = playerTransform.up;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
