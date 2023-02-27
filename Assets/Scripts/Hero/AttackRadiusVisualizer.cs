using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusVisualizer : MonoBehaviour {
	[SerializeField] float radius;
	[SerializeField] float width;
	[SerializeField] int vertexCount;
	
	[SerializeField] float maxDistanceDelta = 4f;
	[SerializeField] float rotateSpeed = 4f;

	Transform target;
	LineRenderer[] lineRenderers;

	void Awake() {
		lineRenderers = GetComponentsInChildren<LineRenderer>();
	}

	void Start() {
		target = LevelManager.getInstance().getHero().transform;

		initializeLineRenderers();
		setVertices();
	}

	void Update() {
		transform.position = Vector2.MoveTowards(transform.position, target.position, maxDistanceDelta * Time.deltaTime);
		transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
	}

	void initializeLineRenderers() {
		foreach (LineRenderer lineRenderer in lineRenderers) {
			lineRenderer.startWidth = width;
			lineRenderer.endWidth = width;
		}
	}

	void setVertices() {
		foreach (LineRenderer lineRenderer in lineRenderers) {
			lineRenderer.positionCount = vertexCount;
			for (int i = 0; i < vertexCount; i++) {
				float angle = (360f / (float) vertexCount) * (float) i;
				lineRenderer.SetPosition(i, radius * new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0));
			}
		}
	}
}
