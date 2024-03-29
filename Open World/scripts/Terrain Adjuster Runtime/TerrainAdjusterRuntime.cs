﻿using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class TerrainAdjusterRuntime : MonoBehaviour
{
    public Terrain terrain;
    public SplineDone pathCreator;
    public float brushFallOff = 0.3f;
    float[,] originalTerrainHeights;

    void Start()
    {
        SaveOriginalTerrainHeights();
    }

    void SaveOriginalTerrainHeights()
    {
        TerrainData terrainData = terrain.terrainData;

        int w = terrainData.heightmapResolution;
        int h = terrainData.heightmapResolution;

        originalTerrainHeights = terrainData.GetHeights(0, 0, w, h);

    }

    void Update()
    {
        if (terrain && pathCreator)
        {
            ShapeTerrain(pathCreator);
        }
    }

   public void ShapeTerrain(SplineDone currentPathCreator)
    {
        SaveOriginalTerrainHeights();
        Terrain currentTerrain = terrain;
        Vector3 terrainPosition = currentTerrain.gameObject.transform.position;
        TerrainData terrainData = currentTerrain.terrainData;

        // both GetHeights and SetHeights use normalized height values, where 0.0 equals to terrain.transform.position.y in the world space and 1.0 equals to terrain.transform.position.y + terrain.terrainData.size.y in the world space
        // so when using GetHeight you have to manually divide the value by the Terrain.activeTerrain.terrainData.size.y which is the configured height ("Terrain Height") of the terrain.
        float terrainMin = currentTerrain.transform.position.y + 0f;
        float terrainMax = currentTerrain.transform.position.y + currentTerrain.terrainData.size.y;
        float totalHeight = terrainMax - terrainMin;

        int w = terrainData.heightmapResolution;
        int h = terrainData.heightmapResolution;

        // clone the original data, the modifications along the path are based on them
        float[,] allHeights = originalTerrainHeights.Clone() as float[,];

        // the blur radius values being used for the various passes
        int[] initialPassRadii = { 15, 7, 2 };

        for (int pass = 0; pass < initialPassRadii.Length; pass++)
        {
            int radius = initialPassRadii[pass];


            // equi-distant points
            List<Vector3> distancePoints = new List<Vector3>();

            // spacing along the array, can speed up the loops
            float arrayIterationSpacing = .1f;

            for (float t = 0; t <= 1; t += arrayIterationSpacing)
            {
                Vector3 point = currentPathCreator.GetPositionAt(t);

                distancePoints.Add(point);
            }

            // sort by height reverse
            // sequential height raising would just lead to irregularities, ie when a higher point follows a lower point
            // we need to proceed from top to bottom height
            distancePoints.Sort((a, b) => -a.y.CompareTo(b.y));

            Vector3[] points = distancePoints.ToArray();

            foreach (var point in points)
            {

                float targetHeight = (point.y - terrainPosition.y) / totalHeight;

                int centerX = (int)(currentPathCreator.transform.position.z + point.z);
                int centerY = (int)(currentPathCreator.transform.position.x + point.x);

                AdjustTerrain(allHeights, radius, centerX, centerY, targetHeight);

            }
        }

        currentTerrain.terrainData.SetHeights(0, 0, allHeights);
    }

    private void AdjustTerrain(float[,] heightMap, int radius, int centerX, int centerY, float targetHeight)
    {
        float deltaHeight = targetHeight - heightMap[centerX, centerY];
        int sqrRadius = radius * radius;

        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        for (int offsetY = -radius; offsetY <= radius; offsetY++)
        {
            for (int offsetX = -radius; offsetX <= radius; offsetX++)
            {
                int sqrDstFromCenter = offsetX * offsetX + offsetY * offsetY;

                // check if point is inside brush radius
                if (sqrDstFromCenter <= sqrRadius)
                {
                    // calculate brush weight with exponential falloff from center
                    float dstFromCenter = Mathf.Sqrt(sqrDstFromCenter);
                    float t = dstFromCenter / radius;
                    float brushWeight = Mathf.Exp(-t * t / brushFallOff);

                    // raise terrain
                    int brushX = centerX + offsetX;
                    int brushY = centerY + offsetY;

                    if (brushX >= 0 && brushY >= 0 && brushX < width && brushY < height)
                    {
                        heightMap[brushX, brushY] += deltaHeight * brushWeight;

                        // clamp the height
                        if (heightMap[brushX, brushY] > targetHeight)
                        {
                            heightMap[brushX, brushY] = targetHeight;
                        }
                    }
                }
            }
        }
    }
}