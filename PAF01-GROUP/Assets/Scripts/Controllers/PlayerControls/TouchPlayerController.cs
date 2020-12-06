using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; 

public class TouchPlayerController : MonoBehaviour
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

    public event Action OnPlayerTargetSet;
    //public event Action rotateCamera; 

    private Vector3 currentPos;

    float touchDuration; 

    [HideInInspector]
    public Touch touch; 

    #endregion

    private void Start()
    {
        currentPos = transform.position;
    }


    void Update()
    {
        

        if (Input.touchCount > 0 )
        {
            touchDuration += Time.deltaTime; 
            touch = Input.GetTouch(0); 
            Vector3 firstTouchPoint = touch.position; 
            //Debug.Log(touchDuration); 

            if(touch.phase == TouchPhase.Ended && touchDuration < 0.2f) //Make sure only short touch and not drag/swipe
            {
                //Debug.Log("StartCoroutine"); 
                //StartCoroutine("singleOrDoubleTap"); 
                SetTargetPosition();
                touchDuration = 0.0f;
            }
            else if(touch.phase == TouchPhase.Moved && touchDuration > 0.5f)
            {
                Vector3 secondTouchPoint = touch.position; 
                //Rotate Camera according to difference between first and second TouchPoint; 
                Debug.Log("TouchMoved"); 
                touchDuration = 0.0f;
                
            }
            
            
        }
        

        if (_isMoving)
        {
            if (Vector3.Distance(transform.position, _targetPos) <= movementRange)
            {
                Move();
            }
            else
                Debug.Log("Area out of player movement range");

        }

    }


//Coroutine that detects a single or a double Tap;
//Might not be needed; 
/*
    IEnumerator singleOrDoubleTap()
    {
        //0.01f does not give enough time for double tap; 
        //But since it is not working currently it is a smoother player movement; 
        //For double tap detection write sth like 0.2f; 
        //Problem: Player waits 0.2f before moving; 

        yield return new WaitForSeconds(0.01f);  
        if(touch.tapCount == 1)
        {
            //Debug.Log("Single"); 
            SetTargetPosition();
        }
        else if(touch.tapCount == 2)
        {
            StopCoroutine("singleOrDoubleTap");
            Debug.Log("DoubleTap"); 
            //rotateCamera(); 
            //
            //With a double tap it should be possible to rotate the camera??
        }
    }
*/
    private void SetTargetPosition()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(touch.position); 
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, whatIsClickable.value))
        {
            //Debug.Log("SetTargetTouch"); 
            currentPos = transform.position;
            _targetPos = hit.transform.position + (transform.up * .5f);
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
        Gizmos.DrawWireSphere(transform.localPosition, movementRange);
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
