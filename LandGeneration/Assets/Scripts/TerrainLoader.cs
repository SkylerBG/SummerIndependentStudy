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

    [SerializeField] PerlinNoiseInputs perlinNoiseInputs;
    [SerializeField] FractalNoiseInputs fractalNoiseInputs;
    [SerializeField] WorleyNoiseInputs worleyNoiseInputs;


    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> terrain = new List<Vector3>();
        switch(terrainType)
        {
            case TerrainType.FlatTerrain:
                terrain = FlatTerrain.GenerateTerrain(terrainLengthAndWidth);
                break;
            case TerrainType.PerlinNoiseTerrain:
                terrain = PerlinNoiseTerrain.GenerateTerrain(FlatTerrain.GenerateTerrain(terrainLengthAndWidth), perlinNoiseInputs);
                break;
            case TerrainType.FractalTerrain:
                terrain = FractalNoiseTerrain.GenerateTerrain(FlatTerrain.GenerateTerrain(terrainLengthAndWidth), fractalNoiseInputs);
                break;
            case TerrainType.WorleyNoiseTerrain:
                worleyNoiseInputs.terrainLengthAndWidth = terrainLengthAndWidth;
                terrain = WorleyNoiseTerrain.GenerateTerrain(FlatTerrain.GenerateTerrain(terrainLengthAndWidth), worleyNoiseInputs);
                break;
            default:
                break;
        }
        LoadTerrain(terrain, terrainLengthAndWidth);
    }

    void LoadTerrain(List<Vector3> terrain, Vector2 lengthAndWidth)
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
        int length = (int)terrainLengthAndWidth.x;
        int width = (int)terrainLengthAndWidth.y;

        int offset = 0;
        for (int z = 0; z < width; z++)
        {
            for (int x = 0; x < length; x++)
            {
                int index = (x + z * width) + offset;
                triangles.AddRange(new int[] { index, index + 1 + width, index + 1 });
                triangles.AddRange(new int[] { index + 1, index + 1 + width, index + 2 + width });
            }
            offset++;
        }
        terrainMesh.triangles = triangles.ToArray();
    }
}