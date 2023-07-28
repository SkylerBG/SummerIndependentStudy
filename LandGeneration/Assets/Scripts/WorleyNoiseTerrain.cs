using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorleyNoiseTerrain
{
    [SerializeField]
    private static int numberOfPoints = 20;
    //Give this function a list of terrain to apply perlin noise to
    public static List<Vector3> GenerateTerrain(List<Vector3> terrain, float terrainScale, Vector2 terrainLengthAndWidth)
    {
        List<Vector2> points = new List<Vector2>();
        for(int i  = 0; i < numberOfPoints; i++)
        {
            points.Add(new Vector2(UnityEngine.Random.Range(0, (int)terrainLengthAndWidth.x), UnityEngine.Random.Range(0, (int)terrainLengthAndWidth.y)));
        }

        Vector3[] terrainArray = terrain.ToArray();
        for(int x = 0; x < terrainLengthAndWidth.x; x++)
        {
            for(int z = 0; z < terrainLengthAndWidth.y; z++)
            {
                int index = z + x * (int)terrainLengthAndWidth.y;
                terrainArray[index].y = WorleyNoise(terrainArray[index].x, terrainArray[index].z, points);
            }    
        }
        return terrainArray.ToList();
    }

    public static float WorleyNoise(float x, float z, List<Vector2> points)
    {
        float[] distances = new float[points.Count];
        for(int i = 0; i < points.Count; i++)
        {
            distances[i] = Vector2.Distance(new Vector2(x, z), points[i]);
        }
        float[] sorted = distances;
        Array.Sort<float>(sorted);

        return sorted[0];
    }
}
