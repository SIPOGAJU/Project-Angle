using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variable Declaration
    [SerializeField] float speed = 10f;
    [Range(0, 5)]
    [SerializeField] float movementRange = 0f;

    public LayerMask whatIsClickable;
    public ParticleSystem playerDeath;

    [HideInInspector]
    public Vector3 _targetPos;
    [HideInInspector]
    public bool _isMoving;

    public event System.Action OnPlayerTargetSet;

    private Vector3 currentPos;
    #endregion

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
            currentPos = transform.position;
            _targetPos = hit.transform.position + (transform.up * 0.5f);
            _isMoving = true;

            if (Vector3.Distance(transform.position, _targetPos) <= movementRange && Vector3.Distance(transform.position, _targetPos) >= 1.5f)
                if (OnPlayerTargetSet != null)
                    OnPlayerTargetSet();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);

        if (Vector3.Distance(_targetPos, transform.position) < .1f)
        {
            //Debug.Log("I'm close enough");
            _isMoving = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, movementRange);
    }

    void OnDeath()
    {
        Destroy(gameObject);
        Instantiate(playerDeath, transform.position, transform.rotation);
        Debug.Log("You have been eliminated for goloso!");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            if (other.gameObject.GetComponent<SimonsPushables>().canMove == true)
            {
                _isMoving = true;
            }

            else
            {
                _isMoving = false;
                transform.position = currentPos;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            _isMoving = false;
            transform.position = currentPos;
        }

        if(collision.gameObject.tag == "Patrol")
        {
            OnDeath();
        }
    }
}
