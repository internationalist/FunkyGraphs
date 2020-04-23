using System;
using UnityEngine;

namespace VectorDraw.Functional
{
    public static class PolygonUtilities
    {

        public static void BuildColliderForDrag(GameObject gameObject, float radius)
        {
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
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

        /// <summary>
        /// <para>Utility function for scaling around a specific pivot point.</para>
        /// <para>Position pivot has to be local to the parent of the target transform to be scaled.</para>
        /// <para>If target is in worldspace then pivot has to be world space coordinates.</para>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="pivot"></param>
        /// <param name="newScale"></param>
        public static void ScaleAround(GameObject target, Vector3 pivot, Vector3 newScale)
        {
            Vector3 A = target.transform.localPosition;
            Vector3 B = pivot;
            Vector3 C = A - B; // diff from object pivot to desired pivot/origin
            float RSx = newScale.x / target.transform.localScale.x; // relative scale factor
            float RSy = newScale.y / target.transform.localScale.y; // relative scale factor
            // calc final position post-scale
            Debug.Log(C);
            Vector3 FPx = B + C * RSx;
            Vector3 FPy = B + C * RSy;
            Vector3 FP = new Vector3(FPx.x, FPy.y, 0);
            Debug.Log(FP);
            // finally, actually perform the scale/translation
            target.transform.localScale = newScale;
            target.transform.localPosition = FP;
        }
    }
}
