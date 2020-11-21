using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolController : MonoBehaviour
{
    [SerializeField] float agentSpeed = 0f;
    public Transform[] wayPoints;

    private int currentPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerController>().OnPlayerTargetSet += OnPlayerMove;

        transform.position = wayPoints[currentPoint].position;
        foreach (var item in wayPoints)
        {
            item.transform.up = transform.up;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentPoint].position, agentSpeed * Time.deltaTime);
    }

    void OnPlayerMove()
    {
        if (currentPoint <= 0)
            currentPoint += 1;

        else if (currentPoint >= wayPoints.Length - 1)
            currentPoint -= 1;
    }
}
