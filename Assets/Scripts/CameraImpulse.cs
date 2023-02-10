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

	public void cameraImpulse(Vector2 direction) {
		direction.Normalize();
		impulseSource.GenerateImpulseWithVelocity(direction * magnitude);
	}
}
