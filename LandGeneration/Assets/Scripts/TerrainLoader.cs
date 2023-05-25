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
    [SerializeField] int terrainLength, terrainWidth, terrainHeight;

    // Start is called before the first frame update
    void Start()
    {
        Square[,] terrain = new Square[terrainLength, terrainWidth];
        switch(terrainType)
        {
            case TerrainType.FlatTerrain:
                terrain = FlatTerrain.GenerateTerrain(terrainLength, terrainWidth);
                break;
            case TerrainType.PerlinNoiseTerrain:
                break;
            case TerrainType.FractalTerrain:
                break;
            case TerrainType.WorleyNoiseTerrain:
                break;
            default:
                break;
        }
        LoadTerrain(terrain);
    }

    void LoadTerrain(Square[,] terrain)
    {
        if (terrain == null || terrain.GetLength(0) == 0 || terrain.GetLength(1) == 0)
            return;
        Mesh terrainMesh;
        for(int x = 0; x < terrain.GetLength(0); x++) 
        {
            for(int y = 0; y < terrain.GetLength(1); y++)
            {

            }
        }
    }
}
