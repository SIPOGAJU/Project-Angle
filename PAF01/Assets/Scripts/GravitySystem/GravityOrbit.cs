using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOrbit : MonoBehaviour
{
    public float gravityIntensity = 60f;
    public bool fixedDirection = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<GravityControl>())
        {
            other.GetComponent<GravityControl>().Gravity = this.GetComponent<GravityOrbit>();
        }
    }
}
