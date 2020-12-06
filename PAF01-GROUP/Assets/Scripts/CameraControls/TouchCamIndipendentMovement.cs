using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchCamIndipendentMovement : MonoBehaviour
{
    public Camera cam;
    [SerializeField] float rotationSensivityY = 2f;
    [SerializeField] float rotationSensivityX = 2f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    [SerializeField] float timer = 2f;
    private float _timer;

    TouchPlayerController player; 

    void Start()
    {
        _timer = timer;
        player = FindObjectOfType<TouchPlayerController>(); 
        //player.rotateCamera += RotateCamera; 
        

    }

    void Update()
    {
       
    }

    public void RotateCamera()
    {

        //Saves first and second point of touch; Calculate difference to rotate the Camera; 
        //
        //
        /* if(player.touch.phase == TouchPhase.Began)
        {
            Vector3 firstTouchPoint = player.touch.position; 
        }
        if(player.touch.phase == TouchPhase.Moved)
        {
            Vector3 secondTouchPoint = player.touch.position; 
            Debug.Log("TouchMoved"); 
        }
        */

        if (Input.GetMouseButton(0))
        {
            cam.gameObject.SetActive(true);

            //Rotate the camera with Moving Finger instead of Mouse
            rotationX += Input.GetAxis("Mouse X") * rotationSensivityX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * rotationSensivityY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, 0f, 90f);
        }
        else
        {
            _timer -= Time.deltaTime;
        }

        if(_timer <= 0)
        {
            cam.gameObject.SetActive(false);
            _timer = timer;
        }

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.right);
    }

}
