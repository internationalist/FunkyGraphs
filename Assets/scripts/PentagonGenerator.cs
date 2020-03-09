using UnityEngine;
using System.Collections;

public class TriangleCreator : MonoBehaviour
{
	public float width = 1;
	public float height = 1;

	public void Start()
	{
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = new Material(Shader.Find("UI/Default"));

		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

		Mesh mesh = new Mesh();

		Vector3[] vertices = new Vector3[3]
		{
			new Vector3(0, 0, 0),
			new Vector3(-.5f, height, 0),
			new Vector3(width, 0, 0),
		};
		mesh.vertices = vertices;

		int[] tris = new int[3]
		{
			// lower left triangle
			0, 2, 1
		};
		mesh.triangles = tris;

		Vector3[] normals = new Vector3[3]
		{
			-Vector3.forward,
			-Vector3.forward,
			-Vector3.forward
		};
		mesh.normals = normals;

		Vector2[] uv = new Vector2[3]
		{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(0, 1),
		};
		mesh.uv = uv;

		meshFilter.mesh = mesh;
		meshRenderer.sharedMaterial.color = Color.black;
	}
}
