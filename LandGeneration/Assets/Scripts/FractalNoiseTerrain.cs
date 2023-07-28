using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct FractalNoiseInputs
{
    public int terrainScale;
    public float octaves;
    public float lacunarity;
    public float persistence;
    public float amplitude;
};
public class FractalNoiseTerrain
{
    public static List<Vector3> GenerateTerrain(List<Vector3> terrain, FractalNoiseInputs inputs)
    {
        Vector3[] terrainArray = terrain.ToArray();
        for (int i = 0; i < terrainArray.Length; i++)
        {
            terrainArray[i].y = FractalNoise(terrainArray[i].x, terrainArray[i].z, inputs.octaves, inputs.lacunarity, inputs.persistence, inputs.terrainScale, inputs.amplitude);
        }

        return terrainArray.ToList();
    }

    //Made using psuedocode from https://devforum.roblox.com/t/creating-procedural-mountains-a-fractal-noise-tutorial/1494362
    public static float FractalNoise(float x, float z, float octaves, float lacunarity, float persistence, int scale, float amplitude)
    {
        //The sum of our octaves
        float value = 0;

        //These coordinates will be scaled the lacunarity
        float x1 = x;
        float z1 = z;

        for (int i = 1; i < octaves; i++)
        {
            //Multiply the noise output by the amplitude and add it to our sum
            value += Mathf.PerlinNoise(x1 / scale, z1 / scale) * amplitude;

            //Scale up our perlin noise by multiplying the coordinates by lacunarity
            z1 *= lacunarity;
            x1 *= lacunarity;

            //Reduce our amplitude by multiplying it by persistence
            amplitude *= persistence;
        }

        // It is possible to have an output value outside of the range[-1, 1]
        //For consistency let's clamp it to that range
        return value*scale;// Mathf.Clamp(value, -1, 1);
    }
}