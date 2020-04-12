using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolygonDrag : MonoBehaviour
{
    public Texture2D moveTexture;
    public Texture2D scaleL;
    public Texture2D scaleR;
    public Texture2D scaleVert;
    public Texture2D scaleHoriz;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;
    public enum InteractionType { SCALENW, SCALEN, SCALENE, SCALEE, SCALESE, SCALES, SCALESW, SCALEW, DRAG, ROTATE};
    public InteractionType interactionType;

    Vector3 m3Init;
    bool interaction;
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
            if (!interaction)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    targetTransform = hit.transform;
                    if (hit.collider.gameObject.CompareTag("Polygon"))
                    {
                        interactionType = InteractionType.DRAG;
                    } else if(hit.collider.gameObject.CompareTag("NW"))
                    {
                        interactionType = InteractionType.SCALENW;
                    }
                    delta = (targetTransform.position - m3);
                    interaction = true;
                }
            }
            if (InteractionType.DRAG.Equals(interactionType))
            {
                targetTransform.position = delta + m3;
            }
        }
        else
        {
            interaction = false;
            targetTransform = null;
        }
    }

    private void OnTargetForDrag()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            switch(hit.collider.gameObject.tag)
            {
                case "Polygon":
                    hotSpot = new Vector2(moveTexture.width / 2, moveTexture.height / 2);
                    Cursor.SetCursor(moveTexture, hotSpot, cursorMode);
                    break;
                case "NW":
                    hotSpot = new Vector2(scaleL.width / 2, scaleL.height / 2);
                    Cursor.SetCursor(scaleL, hotSpot, cursorMode);
                    break;
                case "N":
                    hotSpot = new Vector2(scaleVert.width / 2, scaleVert.height / 2);
                    Cursor.SetCursor(scaleVert, hotSpot, cursorMode);
                    break;
                case "NE":
                    hotSpot = new Vector2(scaleR.width / 2, scaleR.height / 2);
                    Cursor.SetCursor(scaleR, hotSpot, cursorMode);
                    break;
                case "E":
                    hotSpot = new Vector2(scaleHoriz.width / 2, scaleHoriz.height / 2);
                    Cursor.SetCursor(scaleHoriz, hotSpot, cursorMode);
                    break;
                case "SE":
                    hotSpot = new Vector2(scaleL.width / 2, scaleL.height / 2);
                    Cursor.SetCursor(scaleL, hotSpot, cursorMode);
                    break;
                case "S":
                    hotSpot = new Vector2(scaleVert.width / 2, scaleVert.height / 2);
                    Cursor.SetCursor(scaleVert, hotSpot, cursorMode);
                    break;
                case "SW":
                    hotSpot = new Vector2(scaleR.width / 2, scaleR.height / 2);
                    Cursor.SetCursor(scaleR, hotSpot, cursorMode);
                    break;
                case "W":
                    hotSpot = new Vector2(scaleHoriz.width / 2, scaleHoriz.height / 2);
                    Cursor.SetCursor(scaleHoriz, hotSpot, cursorMode);
                    break;
            }
            /*if("Polygon".Equals(hit.collider.gameObject.tag))
            {
                Cursor.SetCursor(moveTexture, hotSpot, cursorMode);
            }
            else if ("NW".Equals(hit.collider.gameObject.tag))
            {
                Cursor.SetCursor(scaleL, hotSpot, cursorMode);
            }*/

        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}