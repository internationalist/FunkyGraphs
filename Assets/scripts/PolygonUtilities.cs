using System;
using UnityEngine;

namespace VectorDraw.Functional
{
    public static class PolygonUtilities
    {

        public static void BuildColliderForDrag(GameObject gameObject, float radius)
        {
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
            Renderer renderer = gameObject.GetComponent<Renderer>();
            Bounds rendBound = renderer.bounds;
            boxCollider.center = rendBound.center;
            boxCollider.size = radius * Vector3.one;
        }

        public static void DrawBorder(GameObject gameObject,
                                      Vector3[] vertices,
                                      Color borderColor,
                                      float borderWidth,
                                      string shader)
        {
            int positionCount = vertices.Length;
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find(shader));
            lineRenderer.widthMultiplier = borderWidth;
            lineRenderer.positionCount = positionCount + 1;
            lineRenderer.startColor = borderColor;
            lineRenderer.endColor = borderColor;
            lineRenderer.loop = true;
            lineRenderer.useWorldSpace = false;
            for (int i = 0; i < vertices.Length; i++)
            {
                lineRenderer.SetPosition(i, vertices[i]);
            }
            lineRenderer.SetPosition(positionCount, vertices[0]);
        }

        public static LineRenderer DrawLine(GameObject gameObject,
                                      Vector3[] vertices,
                                      Color borderColor,
                                      float borderWidth,
                                      string shader)
        {
            LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineRenderer.receiveShadows = false;
            lineRenderer.material = new Material(Shader.Find(shader));
            lineRenderer.widthMultiplier = .02f;
            lineRenderer.positionCount = vertices.Length;
            lineRenderer.startColor = borderColor;
            lineRenderer.endColor = borderColor;
            lineRenderer.loop = false;
            lineRenderer.useWorldSpace = false;
            lineRenderer.SetPositions(vertices);
            return lineRenderer;
        }

        public static BoxCollider AddLineCollider(GameObject line)
        {
            LineRenderer lineRend = line.GetComponent<LineRenderer>();
            BoxCollider coll = new GameObject("Collider").AddComponent<BoxCollider>();
            coll.transform.parent = line.transform;
            SetSizeAndOrient(line.transform.parent, lineRend, coll);
            return coll;
        }

        public static void SetSizeAndOrient(Transform worldParent, LineRenderer lineRend, BoxCollider coll)
        {
            coll.transform.rotation = Quaternion.identity;
            Vector3[] positions = new Vector3[2];
            lineRend.GetPositions(positions);
            Vector3 startPosition = positions[0];
            Vector3 endPosition = positions[1];
            float lineLength = Vector3.Distance(startPosition, endPosition);
            coll.size = new Vector3(lineLength, lineRend.startWidth, 1f);
            Vector3 midPoint = (startPosition + endPosition) / 2;
            coll.transform.position = midPoint;
            //calculate angle between start and end point.
            float angle = (Mathf.Abs(startPosition.y - endPosition.y) / Mathf.Abs(startPosition.x - endPosition.x));

            if ((startPosition.y < endPosition.y && startPosition.x > endPosition.x)
                    || (startPosition.y > endPosition.y && startPosition.x < endPosition.x))
            {
                angle *= -1;
            }

            angle = Mathf.Rad2Deg * Mathf.Atan(angle);
            coll.transform.Rotate(0, 0, angle);
        }


        public static MeshRenderer AddMeshRenderer(GameObject gameObject,
        string shader) {
            MeshRenderer meshRenderer =
                                    gameObject.AddComponent<MeshRenderer>();
            meshRenderer.sharedMaterial =
                                    new Material(Shader.Find("UI/Default"));
            return meshRenderer;
        }

        public static Mesh AddMesh(GameObject gameObject)
        {
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            Mesh mesh = new Mesh();
            meshFilter.mesh = mesh;
            return mesh;
        }

        /// <summary>
        /// <para>Utility function for scaling around a specific pivot point.</para>
        /// <para>Position pivot has to be local to the parent of the target transform to be scaled.</para>
        /// <para>If target is in worldspace then pivot has to be world space coordinates.</para>
        /// </summary>
        /// <param name="target"></param>
        /// <param name="pivot"></param>
        /// <param name="newScale"></param>
        public static void ScaleAround(GameObject target, Vector3 pivot, Vector3 newScale)
        {
            Vector3 A = target.transform.localPosition;
            Vector3 B = pivot;
            Vector3 C = A - B; // diff from object pivot to desired pivot/origin
            float RSx = newScale.x / target.transform.localScale.x; // relative scale factor
            float RSy = newScale.y / target.transform.localScale.y; // relative scale factor
            // calc final position post-scale
            Debug.Log(C);
            Vector3 FPx = B + C * RSx;
            Vector3 FPy = B + C * RSy;
            Vector3 FP = new Vector3(FPx.x, FPy.y, 0);
            Debug.Log(FP);
            // finally, actually perform the scale/translation
            target.transform.localScale = newScale;
            target.transform.localPosition = FP;
        }

        public static void SpawnFigure(string name, GameManager.ObjectType objectType) {

            GameObject polyParent = new GameObject(name);
            Rigidbody parentRB = polyParent.AddComponent<Rigidbody>();
            parentRB.useGravity = false;
            parentRB.isKinematic = true;
            PolygonContainer pc = polyParent.AddComponent<PolygonContainer>();
            pc.lineMarker = GameManager.S.polygonHover;
            GameObject figure;
            PolygonGenerator pg;
            switch (objectType) {
                case GameManager.ObjectType.PENTAGON:
                    figure = new GameObject("Pentagon");
                    pg = figure.AddComponent<PentagonGenerator>();
                    SetupObject(name, parentRB, figure, pg);
                    break;
                case GameManager.ObjectType.SQUARE:
                    figure = new GameObject("Square");
                    pg = figure.AddComponent<SquareGenerator>();
                    SetupObject(name, parentRB, figure, pg);
                    break;
                case GameManager.ObjectType.TRIANGLE:
                    figure = new GameObject("Triangle");
                    pg = figure.AddComponent<TriangleGenerator>();
                    SetupObject(name, parentRB, figure, pg);
                    break;
                default:
                    break;
            }            
        }

        private static void SetupObject(string name, Rigidbody parentRB, GameObject figure, PolygonGenerator pg)
        {
            figure.tag = "Polygon";
            figure.transform.parent = parentRB.transform;
            Rigidbody rb = figure.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            pg.color = Color.red;
            pg.borderColor = Color.black;
            pg.radius = 2f;
            AddHandles(figure, pg);
            GameObject inputField = GameObject.Instantiate(GameManager.S.inputFieldPrefab, Vector3.zero, Quaternion.identity);
            inputField.name = name + "_text";
            inputField.transform.parent = GameManager.S.inputCanvas.transform;
            RectTransform rTransform = inputField.GetComponent<RectTransform>();
            rTransform.localScale = Vector3.one;
            pg.inputField = inputField;
            pg.Initialize();
        }

        private static void AddHandles(GameObject parent, PolygonGenerator pg)
        {
            pg.north = NameInstantiateAndParentHandle(parent.transform, "N");
            pg.northWest = NameInstantiateAndParentHandle(parent.transform, "NW");
            pg.northEast = NameInstantiateAndParentHandle(parent.transform, "NE");
            pg.east = NameInstantiateAndParentHandle(parent.transform, "E");
            pg.southEast = NameInstantiateAndParentHandle(parent.transform, "SE");
            pg.south = NameInstantiateAndParentHandle(parent.transform, "S");
            pg.southWest = NameInstantiateAndParentHandle(parent.transform, "SW");
            pg.west = NameInstantiateAndParentHandle(parent.transform, "W");
        }

        private static GameObject NameInstantiateAndParentHandle(Transform parent, string name)
        {
            GameObject handle = InstantiateHandle();
            handle.name = name;
            handle.transform.parent = parent;
            handle.tag = name;
            return handle;
        }

        private static GameObject InstantiateHandle() {
            return GameObject.Instantiate(GameManager.S.handlePrefab, Vector3.zero, Quaternion.identity);
        }

    }
}
