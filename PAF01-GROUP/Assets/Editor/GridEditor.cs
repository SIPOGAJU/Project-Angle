using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(GridGeneration))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GridGeneration grid = target as GridGeneration;
        if (DrawDefaultInspector())
        {
            grid.GenerateGrid();
        }

        if (GUILayout.Button("Generate Grid"))
        {
            grid.GenerateGrid();
        }

    }
}
