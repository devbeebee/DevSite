using UnityEngine;

public class GridPlacement : Singleton<GridPlacement>
{

    public GameObject cellQuadObj;

    //The size of one cell
    public float cellSize = 1f;
    //How many cells do we have in one row?
    public int gridSize = 20;

    void Start()
    {
        if (!cellQuadObj)
        {
            cellQuadObj = GameObject.CreatePrimitive(PrimitiveType.Quad);
        }
        //Display the grid cells with quads
        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                //The center position of the cell
                Vector3 centerPos = new Vector3(x + cellSize / 2f, 0f, z + cellSize / 2f);

                GameObject newCellQuad = Instantiate(cellQuadObj, centerPos, cellQuadObj.transform.rotation, transform);

                newCellQuad.SetActive(true);
            }
        }
    }

    public bool IsWorldPosInGrid(Vector3 worldPos)
    {
        int gridX =  Mathf.FloorToInt(worldPos.x / cellSize);
        int gridZ =  Mathf.FloorToInt(worldPos.z / cellSize);

        if (gridX >= 0 && gridZ >= 0 && gridX < gridSize && gridZ < gridSize)
        {
            return true;
        }
       return false;
    }
}
