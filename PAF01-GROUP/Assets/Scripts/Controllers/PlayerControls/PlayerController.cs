using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 10f;

    public LayerMask whatIsClickable;

    [Range (0,5)]
    [SerializeField] float movementRange = 0f;

    [HideInInspector]
    public Vector3 _targetPos;
    [HideInInspector]
    public bool _isMoving;

    public event System.Action OnPlayerTargetSet;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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
            _targetPos = hit.transform.position;
            _isMoving = true;

            if (Vector3.Distance(transform.position, _targetPos) <= movementRange && Vector3.Distance(transform.position, _targetPos) >= 1.5f)
                if (OnPlayerTargetSet != null)
                    OnPlayerTargetSet();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(_targetPos, transform.position) < .2f)
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

}
