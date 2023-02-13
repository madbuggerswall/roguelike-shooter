using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusVisualizer : MonoBehaviour {
	[SerializeField] float radius;
	[SerializeField] int vertexCount;

	LineRenderer lineRenderer;

	void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Start() {
		setVertices();
	}

	void Update() {
		transform.Rotate(Vector3.forward * 8f * Time.deltaTime);
	}

	void setVertices() {
		lineRenderer.positionCount = vertexCount;
		for (int i = 0; i < vertexCount; i++) {
			float angle = (360f / (float) vertexCount) * (float) i;
			lineRenderer.SetPosition(i, radius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0));
		}
	}


}
