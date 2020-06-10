using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VectorDraw.Functional;
public class GameManager : MonoBehaviour
{
    public enum ObjectType { SQUARE, TRIANGLE, DIAMOND, PENTAGON, LINE }
    public ObjectType currentObjectToBeCreated;
    private static GameManager _S;
    public GameObject polygonHover;
    public GameObject background;
    BoxCollider backgroundCollider;
    public GameObject handlePrefab;
    public GameObject inputFieldPrefab;
    public GameObject inputCanvas;
    private int objectCount;
    private List<string> objectNames;

    void Awake()
    {
        _S = this;
        objectNames = new List<string>();
    }

    private void Start()
    {
        backgroundCollider = background.GetComponent<BoxCollider>();
        //Setup background collider size.
        SetupBackGroundColliderSize();
    }

    private void SetupBackGroundColliderSize()
    {
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = Camera.main.aspect * screenHeight;
    }

    public static GameManager S
    {
        get
        {
            if (_S == null)
            {
                Debug.LogError("Attempt to access GameManager singleton before initialization");
            }
            return _S;
        }
        set
        {
            if (_S != null)
            {
                Debug.LogError("Redundant attempt to create already initialized singleton. ");
            }
            else
            {
                _S = value;
            }
        }
    }

    public static void CreateFigure(ObjectType typeOfObject) {
        string nameOfObject = typeOfObject.ToString() + "_" + ++_S.objectCount;
        PolygonUtilities.SpawnFigure(nameOfObject, typeOfObject);
    }
}
