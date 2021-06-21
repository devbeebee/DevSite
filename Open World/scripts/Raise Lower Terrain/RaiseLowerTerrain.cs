using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class TerrainNode
{
    public float X = 0;
    public float Y = 0;
}
public class RaiseLowerTerrain : MonoBehaviour
{
    public Transform cube;
    public Terrain myTerrain;
    TerrainData TData => myTerrain.terrainData;
    int heightmapWidth => TData.heightmapResolution;
    int heightmapHeight => TData.heightmapResolution;
    [Range(.01f, 1f)]
    public float height = .5f;
    private float[,] heightmapData = new float[0, 0];

    public float raiseHeightInUnits = 20.0f;
    public Vector2 Look;
    public Vector3 pos;
    public bool lockOn;
    public float currentterrainHeight = 0f;
    public float sampleHeight = 0f;
    public float hitDist = 0f;
    public float terrainMin => myTerrain.transform.position.y + 0f;
    public float terrainMax => myTerrain.transform.position.y + TData.size.y;
    public float totalHeight;
    List<Vector3> PosNotChangedTemp = new List<Vector3>();
    List<Vector3> PosNotChanged = new List<Vector3>();
    public void RaiseHeights(float[,] heightmapData, int x, int y, float terrainHeight, int radius = 15)
    {
        heightmapData[x, y] = terrainHeight;

        int sqrRadius = radius * radius;

        for (int offsetY = -radius; offsetY <= radius; offsetY++)
        {
            for (int offsetX = -radius; offsetX <= radius; offsetX++)
            {
                if (SqrDstFromCenter(offsetX , offsetY) <= sqrRadius && InBounds(heightmapData, x + offsetX, y + offsetY))
                {
                    heightmapData[x + offsetX, y + offsetY] = terrainHeight;
                }
            }
        }
    }
    int SqrDstFromCenter(int offsetX, int offsetY) => offsetX * offsetX + offsetY * offsetY;
    private bool InBounds(float[,] terrainData, int row, int col)
    {
        if (row < terrainData.GetLength(0) - 1 && col < terrainData.GetLength(1) - 1)
        {
            return true;
        }
        return false;
    }
    public void ShapeTerrain(SplineDone spline)
    {
        ResetTerrainHeight(myTerrain);

        totalHeight = terrainMax - terrainMin;
        TerrainData data = myTerrain.terrainData;
        heightmapData = myTerrain.terrainData.GetHeights(0, 0, heightmapWidth, heightmapHeight);
        for (float i = 0; i < 1; i += .1f)
        {
            if (PosNotChanged.Contains(spline.GetPositionAt(i)))
            {
                PosNotChangedTemp.Add(spline.GetPositionAt(i));
            }
        }
        PosNotChanged = PosNotChangedTemp;

        for (float i = 0; i < 1; i += .1f)
        {
            if (!PosNotChanged.Contains(spline.GetPositionAt(i)))
            {
                if (Physics.Raycast(spline.GetPositionAt(i), -Vector3.up, out RaycastHit hitv1))
                {
                    var point = hitv1.point;
                    var x = (int)((point.x / data.size.x) * heightmapWidth);
                    var y = (int)((point.z / data.size.z) * heightmapHeight);
                    float targetHeight = (spline.GetPositionAt(i).y / 600);
                    RaiseHeights(heightmapData, y, x, targetHeight);
                    if (PosNotChanged.Contains(spline.GetPositionAt(i)))
                    {
                        PosNotChanged.Add(spline.GetPositionAt(i));
                    }
                }
            }
        }
      
        SetHeights(heightmapData);
    }
    void SetHeights(float[,] heightmapData) => myTerrain.terrainData.SetHeights(0, 0, heightmapData);
    public void ResetTerrainHeight(Terrain terrain)
    {
        TerrainData terrainData = terrain.terrainData;

        int w = terrainData.heightmapResolution;
        int h = terrainData.heightmapResolution;

        float[,] allHeights = terrainData.GetHeights(0, 0, w, h);


        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                allHeights[y, x] = 0f;
            }
        }

        terrain.terrainData.SetHeights(0, 0, allHeights);
    }
}
