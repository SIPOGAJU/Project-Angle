using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeneration : MonoBehaviour
{
    public Transform tilePrefab;
    [SerializeField] Vector2 gridSize = new Vector2(0f,0f);

    [Range(0, 5)]
    [SerializeField] int tileSize = 0;

    [Range(0, 1)]
    [SerializeField] float outlinePercent = 0f;

    [SerializeField] bool generateHorizontal = true;

    
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    public void GenerateGrid()
    {
        string holderName = "Generated Grid";

        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }

        Transform gridHolder = new GameObject(holderName).transform;
        gridHolder.parent = transform;
        gridHolder.position = transform.position;

        //Spawning Tiles
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector3 CoordToPos;

                if (generateHorizontal == true)
                {
                    CoordToPos = new Vector3((transform.position.x + (tileSize * i)) - (gridSize.x / 2f), transform.position.y, (transform.position.z + (tileSize * j)) - (gridSize.y / 2f));
                }
                else
                {
                    CoordToPos = new Vector3(transform.position.x, (transform.position.x + (tileSize * i)) - (gridSize.x / 2f), (transform.position.z + (tileSize * j)) - (gridSize.y / 2f));
                }
               
                Transform newTile = Instantiate(tilePrefab, CoordToPos, transform.rotation) as Transform;
                newTile.transform.parent = gridHolder;
                newTile.localScale = new Vector3((1f - outlinePercent), newTile.localScale.y, (1f - outlinePercent)) * tileSize;

            }
        }

    }
}
