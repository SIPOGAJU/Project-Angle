using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public GravityOrbit Gravity;
    private new Rigidbody rigidbody;

    public float rotationSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

    }
    private void FixedUpdate()
    {
        if (Gravity)
        {
            Vector3 gravityUp = Vector3.zero;

            if (Gravity.fixedDirection == true)
            {
                gravityUp = Gravity.transform.up;
            }

            else
            {
                gravityUp = (transform.position - Gravity.transform.position).normalized;
            }

            Vector3 localUp = transform.up;

            //Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * transform.rotation;

            transform.up = gravityUp;
            //transform.up = Vector3.Lerp(transform.up, gravityUp, rotationSpeed * Time.deltaTime);

            // push down for gravity

            rigidbody.AddForce((-gravityUp * Gravity.gravityIntensity) * rigidbody.mass);
        }
    }
}
