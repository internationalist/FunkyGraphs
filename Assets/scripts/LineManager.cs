using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDraw.Functional;

public class LineManager : MonoBehaviour
{
    public GameObject lineMarkerEnd;
    public GameObject line;
    public GameObject lineSource;
    public GameObject intersectionMarker;
    private LineRenderer lineRenderer;
    private BoxCollider lineCollider;

    private void FixedUpdate()
    {
        Vector3 m3 = Input.mousePosition;
        m3.z = 10;
        m3 = Camera.main.ScreenToWorldPoint(m3);

        if (Input.GetMouseButton(0))
        {
            lineMarkerEnd.SetActive(true);
            lineMarkerEnd.transform.position = m3;


            Vector3[] lineVerts = { intersectionMarker.transform.position, m3 };
            //lineRenderer.SetPositions(lineVerts);
            if (lineRenderer == null)
            {
                FindIntersectionPoint(lineSource.transform.position, m3);
                lineRenderer = PolygonUtilities.DrawLine(line, lineVerts, Color.gray, .02f, "Sprites/Default");
                lineCollider = PolygonUtilities.AddLineCollider(line);
            } else
            {
                lineRenderer.SetPositions(lineVerts);
            }

        } else
        {
            if (lineRenderer != null)
            {
                PolygonUtilities.SetSizeAndOrient(lineSource.transform, lineRenderer, lineCollider);
            }
        }
    }

    private void FindIntersectionPoint(Vector3 lineStart, Vector3 lineEnd)
    {
        PolygonGenerator pg = lineSource.GetComponentInChildren<PolygonGenerator>();
        Vector3[] verts = pg.GenerateWorldVertices();
        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 polygonSideStart = verts[i];
            Vector3 polygonSideEnd;
            polygonSideEnd = GetPolygonSideEnd(verts, i);


            LineIntersectionManager.IntersectionCheck ic = LineIntersectionManager.DoLinesIntersect(lineStart, lineEnd, polygonSideStart, polygonSideEnd);
            if (ic.didIntersect)
            {
                intersectionMarker.SetActive(true);
                intersectionMarker.transform.position = ic.intersectionPoint;
            }

        }//end for
    }

    private static Vector3 GetPolygonSideEnd(Vector3[] verts, int i)
    {
        Vector3 polygonSideEnd;
        if (i < verts.Length - 1)
        {
            polygonSideEnd = verts[i + 1];
        }
        else
        {
            polygonSideEnd = verts[0];
        }

        return polygonSideEnd;
    }




}
