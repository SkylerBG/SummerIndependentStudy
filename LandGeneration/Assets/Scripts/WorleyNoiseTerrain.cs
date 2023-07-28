using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct WorleyNoiseInputs
{
    public int numberOfPoints;
    [HideInInspector]
    public Vector2 terrainLengthAndWidth;
};
public class WorleyNoiseTerrain
{
    [SerializeField]
    //Give this function a list of terrain to apply perlin noise to
    public static List<Vector3> GenerateTerrain(List<Vector3> terrain, WorleyNoiseInputs inputs)
    {
        List<Vector2> points = new List<Vector2>();
        while(points.Count < inputs.numberOfPoints)
        {
            Vector2 potentialPoint = new Vector2(UnityEngine.Random.Range(0, (int)inputs.terrainLengthAndWidth.x), UnityEngine.Random.Range(0, (int)inputs.terrainLengthAndWidth.y));
            if (!points.Contains(potentialPoint))
            {
                points.Add(potentialPoint);
            }
        }

        Vector3[] terrainArray = terrain.ToArray();
        for(int x = 0; x < inputs.terrainLengthAndWidth.x; x++)
        {
            for(int z = 0; z < inputs.terrainLengthAndWidth.y; z++)
            {
                int index = z + x * (int)inputs.terrainLengthAndWidth.y;
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
