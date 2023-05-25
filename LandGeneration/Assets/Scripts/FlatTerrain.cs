using UnityEngine;

public class FlatTerrain
{
    public static Square[,] GenerateTerrain(int x, int y)
    {
        float offset = 1.0f;
        Square[,] terrainMap = new Square[x, y];

        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                Square square = new Square();
                //the points of the triangle are: top left, bottom left, bottom right
                square.SetLeftTriangle(new Vector3(i, 0, j+offset), new Vector3(i, 0, j), new Vector3(i, 0, j+offset));
                //the points of the triangle are: top left, top right, bottom right
                square.SetRightTriangle(new Vector3(i, 0, j+offset), new Vector3(i+offset, 0, j+offset), new Vector3(i, 0, j+offset));
                terrainMap[i, j] = square;
            }
        }

        return terrainMap;
    }
}
