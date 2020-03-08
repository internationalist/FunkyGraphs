using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCircleMesh : MonoBehaviour {
	[Range(0,360)]
	public int span;
	public Color color;
	// Use this for initialization
	void Start () {
		Vector2 origin = new Vector2 (2, 2);
		Vector2 dest = new Vector2 (7, 2);
		GameObject circle = new GameObject ("circle");
		circle.transform.position = origin;
		MeshRenderer renderer = circle.AddComponent<MeshRenderer> ();
		renderer.sharedMaterial = new Material(Shader.Find("UI/Default"));
		circle.AddComponent<MeshFilter> ();
		Vector3[] vertices = Vertices (origin, dest);
		for (int i = 0; i < span*10; i++) {
			GameObject go = HorizontalMesh.generate (vertices);
			go.transform.parent = circle.transform;
			go.transform.RotateAround (origin, Vector3.forward, i/10);
		}
		CombineMeshes (circle);
		renderer.sharedMaterial.color = new Color(color.r, color.g, color.b);

	}

	private void CombineMeshes(GameObject gameObject) {
		MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length];
		List<GameObject> children = new List<GameObject> ();

		int i = 0;
		while (i < meshFilters.Length)
		{
			combine[i].mesh = meshFilters[i].sharedMesh;
			combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			meshFilters[i].gameObject.SetActive(false);
			children.Add(meshFilters[i].gameObject);
			i++;
		}
		gameObject.transform.GetComponent<MeshFilter>().mesh = new Mesh();
		gameObject.transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
		gameObject.transform.gameObject.SetActive(true);
		foreach (Transform child in gameObject.transform) {
			Destroy (child.gameObject);
		}
	}
	private Vector3[] Vertices (Vector2 origin, Vector2 dest)
	{
		float lineBreadth = .1f;
		Vector3[] vertices = new Vector3[4] {
			new Vector3 (origin.x, origin.y, 0),
			new Vector3 (dest.x, dest.y, 0),
			new Vector3 (origin.x, origin.y + lineBreadth/10f, 0),
			new Vector3 (dest.x, dest.y + lineBreadth, 0)
		};
		return vertices;
	}
}
