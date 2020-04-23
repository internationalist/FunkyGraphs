using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDraw.Functional;

public class PolygonContainer : MonoBehaviour
{
    public GameObject northHandle;
    public GameObject eastHandle;
    public GameObject southHandle;
    public GameObject westHandle;

    // Start is called before the first frame update
    void Start()
    {
        Transform polygon = transform.GetChild(0);
        int childCount = polygon.childCount;
        Vector3 localPosition;
        for(int i = 0; i < childCount; i++)
        {
            Transform child = polygon.GetChild(i);
            switch(child.gameObject.tag)
            {
                case "N":
                    localPosition = child.localPosition;
                    localPosition.y += .2f;
                    northHandle.transform.localPosition = localPosition;
                    break;
                case "E":
                    localPosition = child.localPosition;
                    localPosition.x += .2f;
                    eastHandle.transform.localPosition = localPosition;
                    break;
                case "S":
                    localPosition = child.localPosition;
                    localPosition.y -= .2f;
                    southHandle.transform.localPosition = localPosition;
                    break;
                case "W":
                    localPosition = child.localPosition;
                    localPosition.x -= .2f;
                    westHandle.transform.localPosition = localPosition;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
