using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct PerlinNoiseInputs
{
    public int terrainScale;
};

public class PerlinNoiseTerrain
{
    //Give this function a list of terrain to apply perlin noise to
    public static List<Vector3> GenerateTerrain(List<Vector3> terrain, PerlinNoiseInputs inputs)
    {
        Vector3[] terrainArray = terrain.ToArray();
        for(int i = 0; i < terrainArray.Length; i++)
        {
            terrainArray[i].y = PerlinNoise(terrainArray[i].x, terrainArray[i].z);
            terrainArray[i].y *= inputs.terrainScale;
        }

        return terrainArray.ToList();
    }

    private static float PerlinNoise(float x, float y)
    {
        Vector2 planePoint = new Vector2(x, y);

        // Determine grid cell coordinates
        int x0 = (int)Mathf.Floor(planePoint.x);
        int x1 = x0 + 1;
        int y0 = (int)Mathf.Floor(planePoint.y);
        int y1 = y0 + 1;

        // Determine interpolation weights
        // Could also use higher order polynomial/s-curve here
        float sx = 0.05f;//planePoint.x - x0;
        float sy = 0.05f;//planePoint.y - y0;

        // Interpolate between grid point gradients
        float n0, n1, ix0, ix1, value;

        n0 = DotGridGradient(x0, y0, x, y);
        n1 = DotGridGradient(x1, y0, x, y);
        ix0 = Interpolate(n0, n1, sx);

        n0 = DotGridGradient(x0, y1, x, y);
        n1 = DotGridGradient(x1, y1, x, y);
        ix1 = Interpolate(n0, n1, sx);

        //Debug.Log(ix0 + " " + ix1 + " " + sy);

        value = Interpolate(ix0, ix1, sy);
        return value; // Will return in range -1 to 1. To make it in range 0 to 1, multiply by 0.5 and add 0.5
    }

    // Computes the dot product of the distance and gradient vectors.
    private static float DotGridGradient(int ix, int iy, float x, float y)
    {
        // Get gradient from integer coordinates
        Vector2 gradient = RandomGradient(ix, iy);

        // Compute the distance vector
        float dx = x - (float)ix;
        float dy = y - (float)iy;

        // Compute the dot-product
        return (dx * gradient.x + dy * gradient.y);
    }

    // Create pseudorandom direction vector
    private static Vector2 RandomGradient(int ix, int iy)
    {
        // No precomputed gradients mean this works for any number of grid coordinates
        const uint w = 8 * sizeof(uint);
        const uint s = w / 2; // rotation width
        uint a = (uint)ix, b = (uint)iy;
        a *= 3284157443; b ^= (uint)((int)a << (int)s | (int)a >> ((int)w - (int)s));
        b *= 1911520717; a ^= (uint)((int)b << (int)s | (int)b >> ((int)w - (int)s));
        a *= 2048419325;
        float random = a * (3.14159265f / ~(~0u >> 1)); // in [0, 2*Pi]
        Vector2 v;
        v.x = Mathf.Cos(random); v.y = Mathf.Sin(random);
        return v;
    }

    /* Function to linearly interpolate between a0 and a1
     * Weight w should be in the range [0.0, 1.0]
     */
    private static float Interpolate(float a0, float a1, float w)
    {
        /* // You may want clamping by inserting:
         * if (0.0 > w) return a0;
         * if (1.0 < w) return a1;
         */
        return (a1 - a0) * w + a0;
        /* // Use this cubic interpolation [[Smoothstep]] instead, for a smooth appearance:
         * return (a1 - a0) * (3.0 - w * 2.0) * w * w + a0;
         *
         * // Use [[Smootherstep]] for an even smoother result with a second derivative equal to zero on boundaries:
         * return (a1 - a0) * ((w * (w * 6.0 - 15.0) + 10.0) * w * w * w) + a0;
         */
    }
}