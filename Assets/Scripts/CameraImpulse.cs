using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraImpulse : MonoBehaviour {
	[SerializeField] float magnitude = 0.2f;

	CinemachineImpulseSource impulseSource;

	void Awake() {
		impulseSource = GetComponent<CinemachineImpulseSource>();
	}

	void Start() {
		Events.getInstance().enemyHit.AddListener(cameraImpulse);
		Events.getInstance().playerHit.AddListener(cameraImpulse);
	}


	public void cameraImpulse(Vector2 direction) {
		direction.Normalize();
		impulseSource.GenerateImpulseWithVelocity(direction * magnitude);
	}

	void cameraImpulse(Collision2D collision) {
		Vector2 direction = (collision.collider.transform.position - collision.otherCollider.transform.position).normalized;
		cameraImpulse(direction);
	}
}
