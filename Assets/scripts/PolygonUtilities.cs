using System;
using UnityEngine;

namespace VectorDraw.Functional
{
    public static class PolygonUtilities
    {

        public static void BuildColliderForDrag(GameObject gameObject, float radius)
        {
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            //boxCollider.center = meshRenderer.bounds.center;
            boxCollider.center = Vector3.zero;
            boxCollider.size = radius * Vector3.one;
        }

        public static void DrawBorder(GameObject gameObject,
                                      Vector3[] vertices,
                                      Color borderColor,
                                      float borderWidth,
                                      string shader)
        {
            int positionCount = vertices.Length;
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find(shader));
            lineRenderer.widthMultiplier = borderWidth;
            lineRenderer.positionCount = positionCount + 1;
            lineRenderer.startColor = borderColor;
            lineRenderer.endColor = borderColor;
            lineRenderer.loop = true;
            lineRenderer.useWorldSpace = false;
            for (int i = 0; i < vertices.Length; i++)
            {
                lineRenderer.SetPosition(i, vertices[i]);
            }
            lineRenderer.SetPosition(positionCount, vertices[0]);
        }

        public static MeshRenderer AddMeshRenderer(GameObject gameObject,
            string shader) {
            MeshRenderer meshRenderer =
                                    gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial =
                                    new Material(Shader.Find("UI/Default"));
            return meshRenderer;
        }

        public static Mesh AddMesh(GameObject gameObject)
        {
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            Mesh mesh = new Mesh();
            meshFilter.mesh = mesh;
            return mesh;
        }
    }
}
