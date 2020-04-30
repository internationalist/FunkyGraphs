using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDraw.Functional;


public class PolygonInteraction : MonoBehaviour
{
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;
    public enum InteractionType { SCALENW, SCALEN, SCALENE, SCALEE, SCALESE, SCALES, SCALESW, SCALEW, DRAG, ROTATE, NONE};
    public InteractionType interactionType = InteractionType.NONE;

    Vector3 m3Init;
    bool interaction;
    Vector3 deltaFromMousePos;
    bool onTarget; //this will change from a bool to a type to represent drag, scale horiz, scale vertical and scale.
    Transform targetTransform;

    Vector3 mouseLastPosition;
    private string currentMouseHoverTag;

    public string CurrentMouseHoverTag
    {
        get
        {
            return currentMouseHoverTag;
        }

        set
        {
            currentMouseHoverTag = value;
        }
    }

    private PolygonGenerator polyGenerator;

    public GameObject lineMarkerEnd;
    public GameObject line;
    public GameObject lineSource;
    private LineRenderer lineRenderer;




    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 m3 = Input.mousePosition;
        m3.z = 10;
        m3 = Camera.main.ScreenToWorldPoint(m3);

        Interact(m3);
    }

    private void FindIntersectionPoint(Vector3 lineStart, Vector3 lineEnd)
    {
        PolygonGenerator pg = lineSource.GetComponent<PolygonGenerator>();
        Vector3[] verts = pg.GenerateVertices();
        int[] triangleOrder = pg.GetTriangles();
        for(int i = 0; i < triangleOrder.Length; i++)
        {
            Vector3 triangleSideStart = verts[i];
            Vector3 triangleSideEnd;
            if (i < triangleOrder.Length - 1)
            {
                triangleSideEnd = verts[i + 1];
            } else
            {
                triangleSideEnd = verts[0];
            }
            //covert the line into a equation.
            float x=0, y=0;
            float slope1 = (triangleSideEnd.x - triangleSideStart.x) / (triangleSideEnd.y - triangleSideStart.y);
            float C = triangleSideStart.x - slope1 * triangleSideStart.y;
            float slope2 = (lineEnd.x - lineStart.x) / (lineEnd.y - lineStart.y);
            //slope2 * y = slope1 * y;
        }
    }


    private void Interact(Vector3 m3)
    {
        if (Input.GetMouseButton(0))
        {
            if (!interaction)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    lineMarkerEnd.SetActive(false);
                    targetTransform = hit.transform.parent;
                    if(targetTransform != null) //Because mouse can hover the polygon parent as well.
                    {
                        polyGenerator = targetTransform.GetComponentInChildren<PolygonGenerator>();
                        AssignInteractionType(hit);
                        deltaFromMousePos = (targetTransform.position - m3);
                        interaction = true;
                    }
                } else
                {
                    lineMarkerEnd.SetActive(true);
                    lineMarkerEnd.transform.position = m3;
                    Vector3[] lineVerts = { lineSource.transform.position, m3 };
                    if (lineRenderer != null)
                    {
                        lineRenderer.SetPositions(lineVerts);
                    } else
                    {
                        lineRenderer = PolygonUtilities.DrawLine(line, lineVerts, Color.gray, .02f, "Sprites/Default");
                    }
                }
            }
            if(targetTransform != null)
            {
                if (!mouseLastPosition.Equals(Vector3.zero))
                {
                    switch (interactionType)
                    {
                        case InteractionType.DRAG:
                            targetTransform.position = deltaFromMousePos + m3;
                            break;
                        case InteractionType.SCALENW:
                            ScaleNW(m3);
                            break;
                        case InteractionType.SCALEN:
                            ScaleN(m3);
                            break;
                        case InteractionType.SCALENE:
                            ScaleNE(m3);
                            break;
                        case InteractionType.SCALEE:
                            ScaleE(m3);
                            break;
                        case InteractionType.SCALESE:
                            ScaleSE(m3);
                            break;
                        case InteractionType.SCALES:
                            ScaleS(m3);
                            break;
                        case InteractionType.SCALESW:
                            ScaleSW(m3);
                            break;
                        case InteractionType.SCALEW:
                            ScaleW(m3);
                            break;
                    }
                }
                mouseLastPosition = m3;
            }
        }
        else
        {
            ResetInteractionState();
        }
    }

    private void ScaleNW(Vector3 m3)
    {
        Vector3 delta = m3 - mouseLastPosition;
        delta.z = 0;
        delta.x = -1 * delta.x * .25f;
        delta.y *= .25f;
        Vector3 newScale = targetTransform.localScale + delta;
        PolygonUtilities.ScaleAround(targetTransform.gameObject, polyGenerator.southEast.transform.position, newScale);
    }

    private void ScaleN(Vector3 m3)
    {
        Vector3 delta = m3 - mouseLastPosition;
        delta.y *= .25f;
        delta.x = 0;
        delta.z = 0;
        Vector3 newScale = targetTransform.localScale + delta;
        PolygonUtilities.ScaleAround(targetTransform.gameObject, polyGenerator.south.transform.position, newScale);
    }

    private void ScaleNE(Vector3 m3)
    {
        Vector3 delta = m3 - mouseLastPosition;
        delta.z = 0;
        delta.x *= .25f;
        delta.y *= .25f;
        Vector3 newScale = targetTransform.localScale + delta;
        PolygonUtilities.ScaleAround(targetTransform.gameObject, polyGenerator.southWest.transform.position, newScale);
    }

    private void ScaleE(Vector3 m3)
    {
        Vector3 delta = m3 - mouseLastPosition;
        delta.z = 0;
        delta.x *= .25f;
        delta.y = 0;
        Vector3 newScale = targetTransform.localScale + delta;
        PolygonUtilities.ScaleAround(targetTransform.gameObject, polyGenerator.west.transform.position, newScale);
    }

    private void ScaleSE(Vector3 m3)
    {
        Vector3 delta = m3 - mouseLastPosition;
        delta.z = 0;
        delta.x *= .25f;
        delta.y = -1 * delta.y*.25f;
        Vector3 newScale = targetTransform.localScale + delta;
        PolygonUtilities.ScaleAround(targetTransform.gameObject, polyGenerator.northWest.transform.position, newScale);
    }

    private void ScaleS(Vector3 m3)
    {
        Vector3 delta = m3 - mouseLastPosition;
        delta.y = -1 * delta.y * .25f;
        delta.x = 0;
        delta.z = 0;
        Vector3 newScale = targetTransform.localScale + delta;
        PolygonUtilities.ScaleAround(targetTransform.gameObject, polyGenerator.north.transform.position, newScale);
    }

    private void ScaleSW(Vector3 m3)
    {
        Vector3 delta = m3 - mouseLastPosition;
        delta.y = -1 * delta.y * .25f;
        delta.x = -1 * delta.x * .25f;
        delta.z = 0;
        Vector3 newScale = targetTransform.localScale + delta;
        PolygonUtilities.ScaleAround(targetTransform.gameObject, polyGenerator.northEast.transform.position, newScale);
    }

    private void ScaleW(Vector3 m3)
    {
        Vector3 delta = m3 - mouseLastPosition;
        delta.y = 0;
        delta.x = -1 * delta.x * .25f;
        delta.z = 0;
        Vector3 newScale = targetTransform.localScale + delta;
        PolygonUtilities.ScaleAround(targetTransform.gameObject, polyGenerator.east.transform.position, newScale);
    }

    private void ResetInteractionState()
    {
        interaction = false;
        targetTransform = null;
        interactionType = InteractionType.NONE;
        mouseLastPosition = Vector3.zero;
        lineMarkerEnd.SetActive(false);
    }

    private void AssignInteractionType(RaycastHit hit)
    {
        switch (hit.collider.gameObject.tag)
        {
            case "Polygon":
                interactionType = InteractionType.DRAG;
                break;
            case "NW":
                interactionType = InteractionType.SCALENW;
                break;
            case "N":
                interactionType = InteractionType.SCALEN;
                break;
            case "NE":
                interactionType = InteractionType.SCALENE;
                break;
            case "E":
                interactionType = InteractionType.SCALEE;
                break;
            case "SE":
                interactionType = InteractionType.SCALESE;
                break;
            case "S":
                interactionType = InteractionType.SCALES;
                break;
            case "SW":
                interactionType = InteractionType.SCALESW;
                break;
            case "W":
                interactionType = InteractionType.SCALEW;
                break;
            default:
                interactionType = InteractionType.NONE;
                break;
        }
    }

}