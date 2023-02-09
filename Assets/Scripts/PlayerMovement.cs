using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float movementSpeed = 4f;

	[SerializeField] float squeezeScale = 0.5f;
	[SerializeField] float stretchScale = 1.5f;



	Rigidbody2D rigidBody;
	Vector2 movementDir;
	Coroutine wobblingRoutine;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	// void OnMove(InputValue value) {
	// 	moveDirection = value.Get<Vector2>();
	// }

	void Update() {
		keyboardController();
	}

	void FixedUpdate() {
		rigidBody.MovePosition(rigidBody.position + movementDir * movementSpeed * Time.fixedDeltaTime);
		stride();
	}



	// Strut, walk animation
	void stride() {
		const float angleRange = 16;
		const float freqMul = 4;

		float movement = Mathf.Clamp01(Mathf.Abs(movementDir.x) + Mathf.Abs(movementDir.y));
		rigidBody.MoveRotation(angleRange * Mathf.Sin(freqMul * movementSpeed * movement * Time.time));
	}

	// Prototyping
	void keyboardController() {
		if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) {
			movementDir.y = 1;
		} else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) {
			movementDir.y = -1;
		} else {
			movementDir.y = 0;
		}

		if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) {
			movementDir.x = -1;
		} else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) {
			movementDir.x = 1;
		} else {
			movementDir.x = 0f;
		}
	}
}
