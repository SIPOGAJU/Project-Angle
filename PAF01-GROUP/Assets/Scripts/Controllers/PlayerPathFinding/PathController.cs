using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class PathController : MonoBehaviour
{
    public bool walking = false;
    [Space]

    public Transform currentCube;
    public Transform clickedCube;
    public Transform indicator;

    [Space]
    public LayerMask layerMask;
    public List<Transform> finalPath = new List<Transform>();

    [Space]

    public Ease easType;
    public float moveDelay;
    
    [Space]
    private AudioManager audioManager;
    //public event System.Action OnPlayerStep;
    public int stepCount;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        RayCastDown();
    }

    void Update()
    {

        //GET CURRENT CUBE (UNDER PLAYER)
        RayCastDown();

        if (currentCube.GetComponent<Walkable>().movingGround)
        {
            transform.parent = currentCube.transform;
        }
        else
        {
            transform.parent = null;
        }
        // CLICK ON CUBE

        if (Input.GetButtonDown("Fire1"))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

            if (Physics.Raycast(mouseRay, out mouseHit, 100f,layerMask, QueryTriggerInteraction.Ignore))
            {
                if (mouseHit.transform.GetComponent<Walkable>() != null)
                {
                    clickedCube = mouseHit.transform;
                    DOTween.KillAll();
                    finalPath.Clear();
                    FindPath();

                    indicator.position = mouseHit.transform.GetComponent<Walkable>().GetWalkPoint();
                    Sequence s = DOTween.Sequence();
                    //s.AppendCallback(() => indicator.GetComponentInChildren<ParticleSystem>().Play());
                    s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.white, .1f));
                    s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.grey, .3f).SetDelay(.2f));
                    s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.clear, .3f));
                }
            }
        }
    }

    void FindPath()
    {
        List<Transform> nextCubes = new List<Transform>();
        List<Transform> pastCubes = new List<Transform>();

        foreach (WalkPath path in currentCube.GetComponent<Walkable>().possiblePaths)
        {
            if (path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = currentCube;
            }
        }

        pastCubes.Add(currentCube);

        ExploreCube(nextCubes, pastCubes);
        BuildPath();
    }

    void ExploreCube(List<Transform> nextCubes, List<Transform> visitedCubes)
    {
        Transform current = nextCubes.First();
        nextCubes.Remove(current);

        if (current == clickedCube)
        {
            return;
        }

        foreach (WalkPath path in current.GetComponent<Walkable>().possiblePaths)
        {
            if (!visitedCubes.Contains(path.target) && path.active)
            {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkable>().previousBlock = current;
            }
        }

        visitedCubes.Add(current);

        if (nextCubes.Any())
        {
            ExploreCube(nextCubes, visitedCubes);
        }
    }


    void BuildPath()
    {
        Transform cube = clickedCube;
        while (cube != currentCube)
        {
            finalPath.Add(cube);
            if (cube.GetComponent<Walkable>().previousBlock != null)
                cube = cube.GetComponent<Walkable>().previousBlock;
            else
                return;
        }
        finalPath.Insert(0, clickedCube);
        FollowPath();
    }

    void FollowPath()
    {
        Sequence s = DOTween.Sequence();

        walking = true;

        for (int i = finalPath.Count - 1; i > 0; i--)
        {
            float time = finalPath[i].GetComponent<Walkable>().isStair ? 1.5f : 1;
            s.Append(transform.DOMove(finalPath[i].GetComponent<Walkable>().GetWalkPoint() + transform.up / 2f, moveDelay * time)
                .SetEase(easType)
                .OnComplete(()=>StepMove())
                .SetDelay(.2f));
        }
        s.AppendCallback(() => Clear());
    }

    public void Clear()
    {
        foreach (Transform t in finalPath)
        {
            t.GetComponent<Walkable>().previousBlock = null;
        }
        finalPath.Clear();
        walking = false;
    }

    public void RayCastDown()
    {

        Ray playerRay = new Ray(transform.position, -transform.up);
        RaycastHit playerHit;

        if (Physics.Raycast(playerRay, out playerHit))
        {
            if (playerHit.transform.GetComponent<Walkable>() != null)
            {
                currentCube = playerHit.transform;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pushable"))
        {
            if (other.gameObject.GetComponent<SimonsPushables>().canMove == false)
            {
                Debug.Log("You can not longer continue");
                DOTween.KillAll();
            }
        }
    }

    void StepMove()
    {
        stepCount++;
        audioManager.Play("Walk");
        //if (OnPlayerStep != null)
        //    OnPlayerStep();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Ray ray = new Ray(transform.position, -transform.up);
        Gizmos.DrawRay(ray);
    }
}
