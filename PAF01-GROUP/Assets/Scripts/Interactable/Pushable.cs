using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    [SerializeField] float unitsToMove = 0f;

    private bool isColliding;
    private bool isMoving = false;
    public bool canMove = true;

    private PlayerController player;
    private Vector3 reposition;
    private Vector3 direction;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if(isColliding == true)
        {
            isMoving = true;
        }

        if(isMoving == true)
        {
            Move();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            Debug.Log("Player is pushing");
            isColliding = true;

            Vector3 target = player._targetPos;
            Vector3 playerPos = player.transform.position;
            direction = (target - playerPos).normalized;
            reposition = transform.position + direction * unitsToMove;
        }

        if(collision.gameObject.tag == "Pushable")
        {
            Debug.Log("Collided with pushable");
            isMoving = false;
            StartCoroutine(BackToPosition());
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }
    private void Move()
    {
        if(canMove == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, reposition, Time.deltaTime * unitsToMove);
            if (Vector3.Distance(transform.position, reposition) < 0.2)
            {
                isMoving = false;
            }
        }
    }
    IEnumerator BackToPosition()
    {
        canMove = false;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        transform.position += (-direction * 0.1f);
        yield return new WaitForSeconds(.5f);
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        transform.position += (-direction * 0.1f);
        canMove = true;
    }
}
