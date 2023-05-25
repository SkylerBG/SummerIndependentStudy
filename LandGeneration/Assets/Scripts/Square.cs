using UnityEngine;

public class Square
{
    public void SetLeftTriangle(Vector3 x, Vector3 y, Vector3 z)
    {
        leftTriangle[0] = x;
        leftTriangle[1] = y;
        leftTriangle[2] = z;
    }
    public void SetRightTriangle(Vector3 x, Vector3 y, Vector3 z)
    {
        rightTriangle[0] = x;
        rightTriangle[1] = y;
        rightTriangle[2] = z;
    }

    Vector3[] leftTriangle = new Vector3[3];
    Vector3[] rightTriangle = new Vector3[3];
}
