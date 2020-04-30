using UnityEngine;
using System.Collections;
using VectorDraw.Functional;
using UnityEngine.UI;

public abstract class PolygonGenerator : MonoBehaviour
{
    [Header("This is the radius of the polygon")]
    public float radius = 1;
    private float minY;
    [Header("Border related data")]
    public Color color;
    public Color borderColor;

    [Header("Handles used for scaling.")]
    public GameObject north;
    public GameObject northWest;
    public GameObject northEast;
    public GameObject east;
    public GameObject west;
    public GameObject southWest;
    public GameObject southEast;
    public GameObject south;
    enum HandlePosition { NW, N, NE, E, SE, S, SW, W};
    public Vector3[] vertices;

    //Text related instance variables
    public GameObject inputFieldPrefab;
    private Text txt;
    private InputField iField;
    internal RectTransform rectTransform;
    //internal string textarea;
    public float minTxtX;
    public float minTxtY;
    public Vector3 cachedRendererSize = Vector3.zero;
    private int cachedContentLength;

    private Mesh mesh;

    // Use this for initialization
    void Awake()
    {

        /* Create mesh renderer */
        MeshRenderer meshRenderer = PolygonUtilities.AddMeshRenderer(gameObject, "UI/Default");


        /* Create mesh filter */
        mesh = PolygonUtilities.AddMesh(gameObject);

        vertices = GenerateVertices();

        mesh.vertices = vertices;

        mesh.triangles = GetTriangles();

        mesh.normals = GetNormals();

        mesh.uv = GetUVs();

        meshRenderer.sharedMaterial.color = color;

        /* Create collider for dragging */
        PolygonUtilities.BuildColliderForDrag(gameObject, radius);

        /* Create borders */
        PolygonUtilities.DrawBorder(gameObject, vertices,
                                    borderColor, .08f, "Sprites/Default");

        InitializeTexObjects();

        minY = vertices[0].y;
        Debug.Log(vertices.Length);

        for(int i = 0; i < vertices.Length; i++)
        {
            Debug.Log(vertices[i].y);
            if (minY > vertices[i].y)
            {
                minY = vertices[i].y;
            }
        }
        Debug.Log("minY " + minY);
        resizeHandles(north, HandlePosition.N);
        resizeHandles(northEast, HandlePosition.NE);
        resizeHandles(northWest, HandlePosition.NW);
        resizeHandles(west, HandlePosition.W);
        resizeHandles(east, HandlePosition.E);
        resizeHandles(southEast, HandlePosition.SE);
        resizeHandles(south, HandlePosition.S);
        resizeHandles(southWest, HandlePosition.SW);
    }

    private void resizeHandles(GameObject handle, HandlePosition handlePosition)
    {
        BoxCollider colls = handle.GetComponent<BoxCollider>();
        switch(handlePosition)
        {
            case HandlePosition.NW:
                handle.transform.localPosition = new Vector3(-radius, radius, 0);
                break;
            case HandlePosition.N:
                handle.transform.localPosition = new Vector3(0, radius, 0);
                break;
            case HandlePosition.NE:
                handle.transform.localPosition = new Vector3(radius, radius, 0);
                break;
            case HandlePosition.E:
                handle.transform.localPosition = new Vector3(radius, 0, 0);
                break;
            case HandlePosition.SE:
                handle.transform.localPosition = new Vector3(radius, minY, 0);
                break;
            case HandlePosition.S:
                handle.transform.localPosition = new Vector3(0, minY, 0);
                break;
            case HandlePosition.SW:
                handle.transform.localPosition = new Vector3(-radius, minY, 0);
                break;
            case HandlePosition.W:
                handle.transform.localPosition = new Vector3(-radius, 0, 0);
                break;
        }
    }


    private void LateUpdate()
    {
        ScaleAndAdjustText();
    }

    private void InitializeTexObjects()
    {
        GameObject inputField = inputFieldPrefab;
        rectTransform = inputField.GetComponent<RectTransform>();
        rectTransform.position = transform.position;
        txt = inputField.GetComponentInChildren<Text>();
        iField = inputField.GetComponent<InputField>();
    }

    private void ScaleAndAdjustText()
    {
        rectTransform.position = transform.position;
        Renderer renderer = gameObject.GetComponent<Renderer>();
        if (!renderer.bounds.size.Equals(cachedRendererSize))
        {
            cachedRendererSize = renderer.bounds.size;
            if (renderer.bounds.size.x <= minTxtX && renderer.bounds.size.y > minTxtY)
            {

                txt.rectTransform.sizeDelta = new Vector2(minTxtX * 20, renderer.bounds.size.y * 20);

            }
            else if (renderer.bounds.size.x > minTxtX && renderer.bounds.size.y <= minTxtY)
            {

                txt.rectTransform.sizeDelta = new Vector2(renderer.bounds.size.x * 20, minTxtY * 20);

            }
            else if (renderer.bounds.size.x <= minTxtX && renderer.bounds.size.y <= minTxtY)
            {

                txt.rectTransform.sizeDelta = new Vector2(minTxtX * 20, minTxtY * 20);

            }
            else
            {
                txt.rectTransform.sizeDelta = new Vector2(renderer.bounds.size.x * 20, renderer.bounds.size.y * 20);
            }
        }


        string content = iField.text;
        int fontSize = txt.fontSize;
        float contentSize = content.Length * fontSize * fontSize;
        float txtAreaSize = txt.rectTransform.rect.size.x * txt.rectTransform.rect.size.y * 1.31f;
        if (contentSize > txtAreaSize)//increase the text area to accomodate the content
        {
            float sizex = txt.rectTransform.sizeDelta.x;
            sizex += sizex * 3 / 10;
            float sizey = txt.rectTransform.sizeDelta.y;
            sizey += sizey * 3 / 10;
            txt.rectTransform.sizeDelta = new Vector2(sizex, sizey);
        }
    }

    internal abstract Vector2[] GetUVs();
    public abstract int[] GetTriangles();
    internal abstract Vector3[] GetNormals();
    public abstract Vector3[] GenerateVertices();
}
