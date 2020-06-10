using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMesh
{

    public static GameObject generate(Vector3[] vertices)
    {
        GameObject gameObject = new GameObject();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        //Vector3[] vertices = Vertices(origin, dest);

        mesh.vertices = vertices;

        int[] tris = new int[6]
        {
			// lower left triangle
			0, 2, 1,
			// upper right triangle
			2, 3, 1
        };
        mesh.triangles = tris;

        Vector3[] normals = new Vector3[4]
        {
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward,
            -Vector3.forward
        };
        mesh.normals = normals;

        Vector2[] uv = new Vector2[4]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.uv = uv;

        meshFilter.mesh = mesh;
        return gameObject;
    }
}
