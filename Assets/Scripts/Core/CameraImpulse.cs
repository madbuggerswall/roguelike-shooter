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

	public void impulse(Vector2 direction) {
		direction.Normalize();
		impulseSource.GenerateImpulseWithVelocity(direction * magnitude);
	}

	public void impulse(Collision2D collision) {
		Vector2 direction = (collision.collider.transform.position - collision.otherCollider.transform.position).normalized;
		impulse(direction);
	}
}
