using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingPlayerUp : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] float rotationSpeed = 2f; 

    // Start is called before the first frame update
    void Start()
    {
        transform.up = playerTransform.up;
    }

    // Update is called once per frame
    void Update()
    {
        transform.up = Vector3.Lerp(transform.up, playerTransform.up, Time.deltaTime * rotationSpeed);
    }
}
