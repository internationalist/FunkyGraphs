using UnityEngine;
using System.Collections;
using VectorDraw.Functional;
/*
 * MeshRenderer renderer=gameObject.getcomponent<MeshRenderer>();
 boxCollider.center = renderer.bounds.center;
 boxCollider.size = renderer.bounds.size;
 */
public class PentagonGenerator : PolygonGenerator
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

    internal override int[] GetTriangles()
    {
        int[] tris = new int[9]
        {
			// lower left triangle
			0, 1, 2, 0, 2, 3, 3, 4, 0
        };
        return tris;
    }

    internal override Vector3[] GenerateVertices()
    {
        Vector3[] points = new Vector3[5];
        points[0] = new Vector3(0, radius, 0);
        points[1] = new Vector3(radius * Mathf.Cos(18 * Mathf.Deg2Rad), radius * Mathf.Sin(18 * Mathf.Deg2Rad), 0);
        points[2] = new Vector3(radius * Mathf.Cos(-54 * Mathf.Deg2Rad), radius * Mathf.Sin(-54 * Mathf.Deg2Rad), 0);
        points[3] = new Vector3(radius * Mathf.Cos(234 * Mathf.Deg2Rad), radius * Mathf.Sin(234 * Mathf.Deg2Rad), 0);
        points[4] = new Vector3(radius * Mathf.Cos(162 * Mathf.Deg2Rad), radius * Mathf.Sin(162 * Mathf.Deg2Rad), 0);
        return points;
    }
}
