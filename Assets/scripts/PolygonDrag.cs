using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonDrag : MonoBehaviour
{
    public Texture2D moveTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    Vector3 m3Init;
    bool drag;
    Vector3 delta;
    bool onTarget; //this will change from a bool to a type to represent drag, scale horiz, scale vertical and scale.
    Transform targetTransform;

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 m3 = Input.mousePosition;
        m3.z = 10;
        m3 = Camera.main.ScreenToWorldPoint(m3);

        OnTargetForDrag();

        Drag(m3);
    }

    private void Drag(Vector3 m3)
    {
        if (Input.GetMouseButton(0))
        {
            if (!drag)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("Polygon"))
                    {
                        Cursor.SetCursor(moveTexture, hotSpot, cursorMode);
                        //onTarget = true;
                        targetTransform = hit.transform;
                    }
                    delta = (targetTransform.position - m3);
                    drag = true;
                }
                /*else
                {
                    Cursor.SetCursor(null, Vector2.zero, cursorMode);
                    //onTarget = false;
                }*/
            }
            if (drag)
            {
                targetTransform.position = delta + m3;
            }
        }
        else
        {
            drag = false;
            targetTransform = null;
        }
    }

    private void OnTargetForDrag()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if("Polygon".Equals(hit.collider.gameObject.tag))
            {
                Cursor.SetCursor(moveTexture, hotSpot, cursorMode);
            }

        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}