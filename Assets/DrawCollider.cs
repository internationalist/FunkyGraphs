using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider))]
public class DrawCollider : MonoBehaviour
{
    public Color lineColor;
    public float lineWidth;
    // Start is called before the first frame update
    void Start()
    {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.positionCount = 5;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.loop = true;
        lineRenderer.useWorldSpace = false;
        BoxCollider collider = GetComponent<BoxCollider>();
        Vector3 collSize = collider.size;
        Vector3 one = new Vector3(-1 * collSize.x / 2, collSize.y / 2, -1);
        Vector3 two = new Vector3(collSize.x / 2, collSize.y / 2, -1);
        Vector3 three = new Vector3(collSize.x / 2, -1 * collSize.y / 2, -1);
        Vector3 four = new Vector3(-1 * collSize.x / 2, -1 * collSize.y / 2, -1);
        lineRenderer.SetPosition(0, one);
        lineRenderer.SetPosition(1, two);
        lineRenderer.SetPosition(2, three);
        lineRenderer.SetPosition(3, four);
        lineRenderer.SetPosition(4, one);
    }


}
