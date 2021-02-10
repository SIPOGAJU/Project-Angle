using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public bool walking = false;

    [Space]

    public Transform currentCube;
    public Transform clickedCube;
    public Transform indicator;

    [Space]

    public List<Transform> finalPath = new List<Transform>();

    [Space]
    public LayerMask layerMask;

    private int targetIndex;

    public IEnumerator currentRoutine;
    public event System.Action OnPlayerClick;
    public event System.Action OnGameOver;

    [Space]
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        RayCastDown();
    }

    void Update()
    {
        if (currentCube.GetComponent<Walkable>().isGoalCube)
        {
            if (OnGameOver != null)
                OnGameOver();

            return;
        }

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

        if (walking != true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit mouseHit;

                if (Physics.Raycast(mouseRay, out mouseHit, 100f, layerMask))
                {
                    if (mouseHit.transform.GetComponent<Walkable>() != null)
                    {
                        //Debug.Log("Walkable clicked");
                        clickedCube = mouseHit.transform;
                        finalPath.Clear();
                        FindPath();

                        indicator.position = mouseHit.transform.GetComponent<Walkable>().GetWalkPoint();
                        indicator.transform.up = mouseHit.transform.up;
                        Sequence s = DOTween.Sequence();
                        s.AppendCallback(() => indicator.GetComponentInChildren<ParticleSystem>().Play());
                        s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.white, .1f));
                        s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.black, .3f).SetDelay(.2f));
                        s.Append(indicator.GetComponent<Renderer>().material.DOColor(Color.clear, .3f));
                    }
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


        //finalPath.Insert(0, clickedCube);
        finalPath.Reverse();
        finalPath.Insert(0, currentCube);

        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        if(IsPathStraight(finalPath) == true)
        {
            currentRoutine = FollowPath();
            StartCoroutine(currentRoutine);
            OnPlayerClicked();
        }
        else
        {
            Debug.Log("Path isn't straight, can't move there");
        }
    }
    IEnumerator FollowPath()
    {
        //Debug.Log("coroutine started");
        walking = true;
        while (true)
        {
            if (Vector3.Distance(finalPath[targetIndex].GetComponent<Walkable>().GetWalkPoint() + transform.up / 2f, transform.position) < 0.05f)
            {
                targetIndex++;
                if (targetIndex >= finalPath.Count)
                {
                    Clear();
                    yield break;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, finalPath[targetIndex].GetComponent<Walkable>().GetWalkPoint() + transform.up / 2f, 3f * Time.deltaTime);
            yield return null;
        }
    }

    bool IsPathStraight(List<Transform> path)
    {
        //Assuming the starting node is part of the path, a path of length 2 must be straight
        if (path.Count <= 1) return true;

        //Get the first direction that we walk in. We will use this to compare the direction for the rest of the nodes
        Vector3 startingDir = path[1].position - path[0].position;

        //Get the direction to each node from its previous node, then compare it to the direction of our starting position
        for (int i = 2; i < path.Count; i++)
        {
            Vector3 dir = path[i].position - path[i - 1].position;
            float dotProduct = Mathf.Abs(Vector3.Dot(startingDir, dir));
            //Debug.Log(dotProduct);
            if (dotProduct < .8f) return false; //This means the path is not straight
        }
        return true;
    }
    public void Clear()
    {
        targetIndex = 0;
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

    private void OnPlayerClicked()
    {
        if(audioManager != null)
            audioManager.Play("Click");
        if (OnPlayerClick != null)
            OnPlayerClick();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Ray ray = new Ray(transform.position, -transform.up);
        Gizmos.DrawRay(ray);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInParent<PivotController>() != null)
        {
            other.gameObject.GetComponentInParent<PivotController>().canRotate = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponentInParent<PivotController>() != null)
        {
            other.gameObject.GetComponentInParent<PivotController>().canRotate = true;
        }
    }
}