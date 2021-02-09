using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PivotController : MonoBehaviour
{
    [SerializeField] float timer = 3f;
    private float waitTime;
    public bool canRotate = true;

    void Start()
    {
        waitTime = 2f;
    }

    void Update()
    {
        if(canRotate)
        {
            if (Time.time > waitTime)
            {
                waitTime = Time.time + timer;
                transform.DORotate(new Vector3(0, 0, 90), .6f, RotateMode.WorldAxisAdd).SetEase(Ease.OutBack);
            }
        }
    }
}
