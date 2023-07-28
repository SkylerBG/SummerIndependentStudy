using UnityEngine;
using System.Collections.Generic;

public class FlatTerrain
{
    public static List<Vector3> GenerateTerrain(Vector2 lengthAndWidth)
    {
        List<Vector3> terrainMap = new List<Vector3>();

        float width = lengthAndWidth.x;
        float length = lengthAndWidth.y; 

        for(float z = 0; z <= width; z++)
        {
            for(float x = 0; x <= length; x++)
            {
                terrainMap.Add(new Vector3(x,0.0f,z));
            }
        }
        return terrainMap;
    }
}
