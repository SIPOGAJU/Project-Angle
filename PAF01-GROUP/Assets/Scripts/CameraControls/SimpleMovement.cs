using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
  
    Camera cam;

    Vector3 camF;
    Vector3 camR;


    public float playerSpeed = 10f;

    private Vector3 movDir;

    private Vector2 getInput;

    private void Awake()
    {
    }

    // Start is called before the first frame update


    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        getInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //camF = cam.transform.forward;
        //camR = cam.transform.right;

        //camF.y = 0f;

        //camF = camF.normalized;
        //camR = camR.normalized;

        //movDir = (camF * getInput.y + camR * getInput.x);

        movDir = new Vector3(getInput.x, 0, getInput.y);

    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position += movDir * playerSpeed * Time.deltaTime;
    }
}
