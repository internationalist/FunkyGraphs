using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonDrag : MonoBehaviour
{
    Vector3 m3Init;
    bool drag;
    Vector3 delta;
    public Texture2D moveTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 m3 = Input.mousePosition;
        m3.z = 10;
        m3 = Camera.main.ScreenToWorldPoint(m3);

        if (Physics.Raycast(ray, out hit))
        {
            Cursor.SetCursor(moveTexture, hotSpot, cursorMode);
            if (Input.GetMouseButton(0))
            {
                if (!drag)
                {
                    delta = (hit.transform.position - m3);
                    drag = true;

                }
                hit.transform.position = delta + m3;
            } else
            {
                drag = false;
            }
        } else
        {
            drag = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}