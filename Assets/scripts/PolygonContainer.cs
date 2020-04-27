using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDraw.Functional;

public class PolygonContainer : MonoBehaviour
{
    public GameObject lineMarker;
    private PentagonGenerator pentagonGen;

    // Start is called before the first frame update
    void Start()
    {
        Transform polygon = transform.GetChild(0);
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.size = polygon.GetComponent<Renderer>().bounds.size*1.01f;
        pentagonGen = GetComponentInChildren<PentagonGenerator>();
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        if (hits != null && hits.Length > 0)
        {
            foreach(RaycastHit hit in hits)
            {
                if(hit.collider.gameObject.tag.Equals("PolygonContainer"))
                {
                    lineMarker.SetActive(true);
                    lineMarker.transform.position = hit.point;
                    Debug.Log("Mouse at " + hit.point);
                    Debug.Log("Vertices 2 is " + transform.TransformPoint(pentagonGen.vertices[1]));
                } else
                {
                    lineMarker.SetActive(false);
                }
            }
        } else
        {
            lineMarker.SetActive(false);
        }

    }


}
