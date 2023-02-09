using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float movementSpeed = 4f;

	[SerializeField] float squeezeScale = 0.5f;
	[SerializeField] float stretchScale = 1.5f;

	[SerializeField] float wobbleDuration = 0.5f;
	[SerializeField] float wobbleSpeed = 20f;
	[SerializeField] float wobbleAmount = 0.05f;

	Rigidbody2D rigidBody;
	Vector2 movementDir;
	Vector3 originalScale;
	bool isWobbling = false;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		originalScale = transform.localScale;
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
		// squeezeAndStretch();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (!isWobbling) {
			StartCoroutine(Wobble());
		}
	}

	// Strut, walk animation
	void stride() {
		const float angleRange = 16;
		const float freqMul = 4;

		float movement = Mathf.Clamp01(Mathf.Abs(movementDir.x) + Mathf.Abs(movementDir.y));
		rigidBody.MoveRotation(angleRange * Mathf.Sin(freqMul * movementSpeed * movement * Time.time));
	}

	void squeezeAndStretch() {
		float squeezeMul = 0.1f;
		float stretchMul = 0.1f;

		(float x, float y) scale;
		scale.x = stretchMul * Mathf.Abs(movementDir.x) - squeezeMul * Mathf.Abs(movementDir.y);
		scale.y = stretchMul * Mathf.Abs(movementDir.y) - squeezeMul * Mathf.Abs(movementDir.x);
		transform.localScale = originalScale + new Vector3(scale.x, scale.y, 0);
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

	private IEnumerator Wobble() {
		isWobbling = true;
		float elapsedTime = 0f;
		while (elapsedTime < wobbleDuration) {
			transform.localScale = originalScale + Vector3.one * Mathf.Sin(elapsedTime * wobbleSpeed) * wobbleAmount;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = originalScale;
		isWobbling = false;
	}
}
