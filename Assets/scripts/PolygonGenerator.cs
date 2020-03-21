using UnityEngine;
using System.Collections;
using VectorDraw.Functional;
using UnityEngine.UI;

public abstract class PolygonGenerator : MonoBehaviour
{
    public float radius = 1;
    public float height = 1;
    public Color color;
    public Color borderColor;
    public GameObject inputFieldPrefab;
    private Text txt;
    private InputField iField;
    internal RectTransform rectTransform;
    //internal string textarea;
    public float minTxtX;
    public float minTxtY;
    public Vector3 cachedRendererSize = Vector3.zero;
    private int cachedContentLength;
    // Use this for initialization
    void Start()
    {

        /* Create mesh renderer */
        MeshRenderer meshRenderer = PolygonUtilities.AddMeshRenderer(gameObject, "UI/Default");


        /* Create mesh filter */
        Mesh mesh = PolygonUtilities.AddMesh(gameObject);

        Vector3[] vertices = GenerateVertices();

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

        GameObject inputField = Instantiate(inputFieldPrefab, transform.position, Quaternion.identity);
        inputField.transform.SetParent(GameObject.Find("Canvas").transform, false);
        rectTransform = inputField.GetComponent<RectTransform>();
        rectTransform.position = transform.position;
        txt = inputField.GetComponentInChildren<Text>();
        iField = inputField.GetComponent<InputField>();
    }

    private void LateUpdate()
    {
        rectTransform.position = transform.position;
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Debug.Log(renderer.bounds.size);
        if(!renderer.bounds.size.Equals(cachedRendererSize))
        {
            Debug.Log("RESIZING FOR RENDER");
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
        float contentSize = content.Length*fontSize*fontSize;
        Debug.Log("content size " + contentSize + " " + content);
        Debug.Log("textarea size " +  txt.rectTransform.rect.size.x * txt.rectTransform.rect.size.y*1.31f);
        float txtAreaSize = txt.rectTransform.rect.size.x * txt.rectTransform.rect.size.y * 1.31f;
        if(contentSize > txtAreaSize)//increase the text area to accomodate the content
        {
            float sizex = txt.rectTransform.sizeDelta.x;
            sizex += sizex * 3 / 10;
            float sizey = txt.rectTransform.sizeDelta.y;
            sizey += sizey * 3 / 10;
            Debug.Log("Resizing text area");
            txt.rectTransform.sizeDelta = new Vector2(sizex, sizey);
        }
    }

    internal abstract Vector2[] GetUVs();
    internal abstract int[] GetTriangles();
    internal abstract Vector3[] GetNormals();
    internal abstract Vector3[] GenerateVertices();
}
