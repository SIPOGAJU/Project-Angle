using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIndependentMovement : MonoBehaviour
{
    public Camera cam;
    [SerializeField] float rotationSensivityY = 2f;
    [SerializeField] float rotationSensivityX = 2f;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    [SerializeField] float timer = 2f;
    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        _timer = timer;
    }

    // Update is called once per frame
    void Update()
    {

        bool rotating = Input.GetButton("Fire2");
        if (rotating)
        {
            cam.gameObject.SetActive(true);

            rotationX += Input.GetAxis("Mouse X") * rotationSensivityX * Time.deltaTime;
            rotationY += Input.GetAxis("Mouse Y") * rotationSensivityY * Time.deltaTime;
            rotationY = Mathf.Clamp(rotationY, 0f, 90f);

            transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.right);

            Debug.Log(rotationX);
            Debug.Log(rotationY);
        }
        else if (!rotating)
        {
            _timer -= Time.deltaTime;
        }

        if (_timer <= 0)
        {
            cam.gameObject.SetActive(false);
            _timer = timer;
        }

        

        
    }
}
