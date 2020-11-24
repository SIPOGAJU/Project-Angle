﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera playerCam;
    public CinemachineVirtualCamera orbitCam;
    public Transform playerUp;
    
    [SerializeField] float rotationSpeed = 1f;
    [SerializeField] float waitTime = 2f;
    private float backToPlayerCam;

    private Vector3 originalUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        float sideRotation = Input.GetAxisRaw("Horizontal") * rotationSpeed;
        float upRotation = Input.GetAxisRaw("Vertical") * rotationSpeed;
        
        
        if (sideRotation != 0 && upRotation == 0)
        {
            transform.Rotate(0f, 0f, -sideRotation);
            orbitCam.Priority = 11;
            backToPlayerCam = waitTime;
        }

        else if(upRotation != 0 && sideRotation == 0)
        {
            transform.Rotate(upRotation, 0f, 0f);
            Debug.Log(upRotation);
            orbitCam.Priority = 11;
            backToPlayerCam = waitTime;
        }

        if(sideRotation == 0f && upRotation == 0f)
        {
            backToPlayerCam -= Time.deltaTime;

            if(backToPlayerCam <= 0)
            {
                orbitCam.Priority = 5;
                
                if(playerUp != null)
                    transform.up = playerUp.transform.up;
            }
        }
    }
}
