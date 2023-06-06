using UnityEngine;
using System.Collections.Generic;

public class FlatTerrain
{
    public static List<Vector3> GenerateTerrain(Vector2 lengthAndWidth, int resolution)
    {
        //4 is for the verticies of the square
        List<Vector3> terrainMap = new List<Vector3>();

        float xPerStep = lengthAndWidth.x / resolution;
        float yPerStep = lengthAndWidth.y / resolution;
        for (int y = 0; y < resolution + 1; y++)
        {
            for(int x = 0; x < resolution + 1; x++)
            {
                //Debug.Log(i + " " + j);
                //terrainMap[width * i + j] = new Vector3(i, 0, j);
                terrainMap.Add(new Vector3(x*xPerStep, 0, y*yPerStep));
            }
        }
        return terrainMap;
    }
}
