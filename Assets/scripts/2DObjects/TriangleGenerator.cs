using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGenerator : PolygonGenerator
{
    internal override Vector2[] GetUVs()
    {
        return new Vector2[5]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(0, 0)
        };
    }

    internal override Vector3[] GetNormals()
    {
        return new Vector3[5]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
    }

    public override int[] GetTriangles()
    {
        int[] tris = new int[3]
        {
			// lower left triangle
			0, 1, 2
        };
        return tris;
    }

    public override Vector3[] GenerateVertices()
    {
        Vector3[] points = new Vector3[3];
        points[0] = new Vector3(-radius, 0, 0);
        points[1] = new Vector3(0, radius, 0);
        points[2] = new Vector3(radius, 0, 0);
        return points;
    }
}
