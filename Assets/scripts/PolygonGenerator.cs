using UnityEngine;
using System.Collections;
using VectorDraw.Functional;

public abstract class PolygonGenerator : MonoBehaviour
{
    public float radius = 1;
    public float height = 1;
    public Color color;
    public Color borderColor;
    // Use this for initialization
    void Start()
    {

        /* Create mesh renderer */
        MeshRenderer meshRenderer = PolygonUtilities.AddMeshRenderer(gameObject, "UI/Default");

        /* Create mesh filter */
        Mesh mesh = PolygonUtilities.AddMesh(gameObject);

        Vector3[] vertices = GenerateVertices();

        mesh.vertices = vertices;

        mesh.triangles = GetTriangles();

        mesh.normals = GetNormals();

        mesh.uv = GetUVs();

        meshRenderer.sharedMaterial.color = color;

        /* Create collider for dragging */
        PolygonUtilities.BuildColliderForDrag(gameObject, radius);

        /* Create borders */
        PolygonUtilities.DrawBorder(gameObject, vertices,
                                    borderColor, .05f, "Sprites/Default");
    }

    internal abstract Vector2[] GetUVs();
    internal abstract int[] GetTriangles();
    internal abstract Vector3[] GetNormals();
    internal abstract Vector3[] GenerateVertices();
}
