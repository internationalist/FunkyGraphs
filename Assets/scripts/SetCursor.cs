using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonInteraction))]
public class SetCursor : MonoBehaviour
{
    public Texture2D moveTexture;
    public Texture2D scaleL;
    public Texture2D scaleR;
    public Texture2D scaleVert;
    public Texture2D scaleHoriz;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot;
    private PolygonInteraction polygonDrag;

    private void Start()
    {
        polygonDrag = GetComponent<PolygonInteraction>();
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (PolygonInteraction.InteractionType.NONE.Equals(polygonDrag.interactionType))
        {
            ChangeCursor();
        }
    }

    private void ChangeCursor()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.gameObject.tag.Equals(polygonDrag.CurrentMouseHoverTag))
            {
                switch (hit.collider.gameObject.tag)
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
                polygonDrag.CurrentMouseHoverTag = hit.collider.gameObject.tag;
            }
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            polygonDrag.CurrentMouseHoverTag = null;
            polygonDrag.interactionType = PolygonInteraction.InteractionType.NONE;
        }
    }
}
