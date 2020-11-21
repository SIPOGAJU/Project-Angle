using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Quaternion _targetRot;
    [SerializeField] float rotationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _targetRot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRot, rotationSpeed * Time.deltaTime);
    }

    public void RotateUp()
    {
        _targetRot *= Quaternion.AngleAxis(90, Vector3.right);
    }
    public void RotateDown()
    {
        _targetRot *= Quaternion.AngleAxis(-90, Vector3.right);
    }

    public void RotateLeft()
    {
        _targetRot *= Quaternion.AngleAxis(90, Vector3.up);
    }
    public void RotateRight()
    {
        _targetRot *= Quaternion.AngleAxis(-90, Vector3.up);
    }
}
