using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 endPosition;

    public Vector3 StartPosition
    {
        get
        {
            return startPosition;
        }

        set
        {
            startPosition = value;
        }
    }

    public Vector3 EndPosition
    {
        get
        {
            return endPosition;
        }

        set
        {
            endPosition = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
