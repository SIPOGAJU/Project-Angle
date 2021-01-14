using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ConditionManager : MonoBehaviour
{
    public static ConditionManager instance;

    public PlayerController player;
    public List<PathCondition> pathConditions = new List<PathCondition>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        foreach (PathCondition pc in pathConditions)
        {
            int count = 0;
            for (int i = 0; i < pc.conditions.Count; i++)
            {
                if (pc.conditions[i].conditionObject.position == pc.conditions[i].position)
                {
                    count++;
                }
            }
            foreach (SinglePath sp in pc.paths)
                sp.block.possiblePaths[sp.index].active = (count == pc.conditions.Count);
        }

        if (player.walking)
            return;

    }
}

[System.Serializable]
public class PathCondition
{
    public string pathConditionName;
    public List<Condition> conditions;
    public List<SinglePath> paths;
}
[System.Serializable]
public class Condition
{
    public Transform conditionObject;
    public Vector3 position;

}
[System.Serializable]
public class SinglePath
{
    public Walkable block;
    public int index;
}
