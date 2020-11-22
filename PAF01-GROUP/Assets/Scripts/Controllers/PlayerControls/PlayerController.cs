using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variable Declaration
    Rigidbody rb;
    [SerializeField] float speed = 10f;
    [Range(0, 5)]
    [SerializeField] float movementRange = 0f;
    [SerializeField] float rayLenght = 0f;

    public LayerMask whatIsClickable;
    public LayerMask staticObstacle;

    [HideInInspector]
    public Vector3 _targetPos;
    [HideInInspector]
    public bool _isMoving;

    public event System.Action OnPlayerTargetSet;

    private Vector3 fwd, bck, rgt, lft;
    private Vector3 currentPos;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentPos = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetTargetPosition();
        }

        if (_isMoving)
        {

            if (Vector3.Distance(transform.position, _targetPos) <= movementRange)
                Move();
            else
                Debug.Log("Area out of player movement range");

        }
    }

    private void SetTargetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, whatIsClickable.value))
        {
            _targetPos = hit.transform.position + (transform.up * 0.5f);
            currentPos = transform.position;
            _isMoving = true;

            if (Vector3.Distance(transform.position, _targetPos) <= movementRange && Vector3.Distance(transform.position, _targetPos) >= 1.5f)
                if (OnPlayerTargetSet != null)
                    OnPlayerTargetSet();
        }
    }

    private void Move()
    {
        checkHits();

        transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);


        if (Vector3.Distance(_targetPos, transform.position) < .1f)
        {
            //Debug.Log("I'm close enough");
            _isMoving = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, movementRange);
        Gizmos.color = Color.green;
    }

    private void checkHits()
    {
        RaycastHit hit;

        fwd = transform.forward;
        rgt = transform.right;
        lft = -transform.right;
        bck = -transform.forward;

        //Detecting collision with static objects

        if (Physics.Raycast(transform.position, fwd, out hit, rayLenght, staticObstacle.value))
        {
            transform.position += transform.TransformDirection(Vector3.back * .2f);
            _isMoving = false;
            Debug.Log("Can't move in that direction");
        }

        if (Physics.Raycast(transform.position, rgt, out hit, rayLenght, staticObstacle.value))
        {
            transform.position += transform.TransformDirection(Vector3.left * .2f);
            _isMoving = false;
            Debug.Log("Can't move in that direction");
        }

        if (Physics.Raycast(transform.position, lft, out hit, rayLenght, staticObstacle.value))
        {
            transform.position += transform.TransformDirection(Vector3.right * .2f);
            _isMoving = false;
            Debug.Log("Can't move in that direction");
        }
        if (Physics.Raycast(transform.position, bck, out hit, rayLenght, staticObstacle.value))
        {
            transform.position += transform.TransformDirection(Vector3.forward * .2f);
            _isMoving = false;
            Debug.Log("Can't move in that direction");
        }

    //     //Detecting collision with pushable objects and checking whether they are able to move one step further or not

    //     if (Physics.Raycast(transform.position, fwd, out hit, rayLenght))
    //     {
    //         if(hit.collider.gameObject.tag == "Pushable")
    //         {
    //             if (hit.collider.GetComponent<SimonsPushables>().canMove == false) 
    //             {
    //                 transform.position = currentPos;
    //                 _isMoving = false;
    //                 Debug.Log("Can't move in that direction");
    //             }
    //         }
    //     }
    //     if (Physics.Raycast(transform.position, bck, out hit, rayLenght))
    //     {
    //         if (hit.collider.gameObject.tag == "Pushable")
    //         {
    //             if (hit.collider.GetComponent<SimonsPushables>().canMove == false)
    //             {
    //                 transform.position = currentPos;
    //                 _isMoving = false;
    //                 Debug.Log("Can't move in that direction");
    //             }
    //         }
    //     }
    //     if (Physics.Raycast(transform.position, lft, out hit, rayLenght))
    //     {
    //         if (hit.collider.gameObject.tag == "Pushable")
    //         {
    //             if (hit.collider.GetComponent<SimonsPushables>().canMove == false)
    //             {
    //                 transform.position = currentPos;
    //                 _isMoving = false;
    //                 Debug.Log("Can't move in that direction");
    //             }
    //         }
    //     }
    //     if (Physics.Raycast(transform.position, rgt, out hit, rayLenght))
    //     {
    //         if (hit.collider.gameObject.tag == "Pushable")
    //         {
    //             if (hit.collider.GetComponent<SimonsPushables>().canMove == false)
    //             {
    //                 transform.position = currentPos;
    //                 _isMoving = false;
    //                 Debug.Log("Can't move in that direction");
    //             }
    //         }
    //     }
     }

}
