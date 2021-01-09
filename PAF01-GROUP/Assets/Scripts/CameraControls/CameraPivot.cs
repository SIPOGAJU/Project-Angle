using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public Transform playerTransform;
    [SerializeField] float rotationSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {

        if (playerTransform != null)
            transform.rotation = playerTransform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
            transform.up = Vector3.Lerp(transform.up, playerTransform.up, Time.deltaTime * rotationSpeed);
    }
}
