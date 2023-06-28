using System.Collections.Generic;
using UnityEngine;

public class TerrainLoader : MonoBehaviour
{
    enum TerrainType
    {
        FlatTerrain,
        PerlinNoiseTerrain,
        FractalTerrain,
        WorleyNoiseTerrain
    }

    [SerializeField] TerrainType terrainType = TerrainType.FlatTerrain;
    [SerializeField] Vector2 terrainLengthAndWidth;
    [SerializeField] int resolution, terrainHeight;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> terrain = new List<Vector3>();
        switch(terrainType)
        {
            case TerrainType.FlatTerrain:
                terrain = FlatTerrain.GenerateTerrain(terrainLengthAndWidth, resolution);
                break;
            case TerrainType.PerlinNoiseTerrain:
                terrain = PerlinNoiseTerrain.GenerateTerrain(FlatTerrain.GenerateTerrain(terrainLengthAndWidth, resolution), terrainHeight);
                break;
            case TerrainType.FractalTerrain:
                break;
            case TerrainType.WorleyNoiseTerrain:
                break;
            default:
                break;
        }
        LoadTerrain(terrain, resolution);
    }

    void LoadTerrain(List<Vector3> terrain, int resolution)
    {
        if (terrain == null || terrain.Count == 0)
            return;
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        Mesh terrainMesh = GetComponent<MeshFilter>().mesh;
        terrainMesh.Clear();

        terrainMesh.vertices = terrain.ToArray();

        List<Vector2> uvs = new List<Vector2>();
        foreach(Vector3 v in terrainMesh.vertices)
        {
            uvs.Add(new Vector2(v.x, v.z));
        }
        terrainMesh.uv = uvs.ToArray();

        gameObject.GetComponent<MeshRenderer>().material.color = Color.white;

        List<int> triangles = new List<int>(); 
        for(int row = 0; row < resolution; row++)
        {
            for(int col = 0; col < resolution; col++)
            {
                int i = (row * resolution) + row + col;
                triangles.Add(i);
                triangles.Add(i + resolution + 1);
                triangles.Add(i + resolution + 2);

                triangles.Add(i);
                triangles.Add(i + resolution + 2);
                triangles.Add(i+1);
            }
        }

        terrainMesh.triangles = triangles.ToArray();

    }
}


//foreach(Vector3 point in terrain)
//{
//    Debug.Log(point);
//}

//terrainMesh.vertices = terrain.ToArray();
//int[] triangles = new int[terrain.Count * 3 / 2];

//for (int i = 0; i < triangles.Length; i += 6)
//{
//    triangles[i] = i;
//    triangles[i + 1] = i + 1;
//    triangles[i + 2] = (i + 1) + (width * 2);
//    triangles[i + 3] = (i + 1) + (width * 2);
//    triangles[i + 4] = i + (width * 2);
//    triangles[i + 5] = i;
//}

//for(int i = 0; i < triangles.Length; i++)
//{
//    Debug.Log("triangles[" + i + "]: " + triangles[i]);
//}