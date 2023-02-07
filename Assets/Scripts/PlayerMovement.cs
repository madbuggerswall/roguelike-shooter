using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] float moveSpeed = 5f;

	[SerializeField] float squeezeScale = 0.5f;
	[SerializeField] float stretchScale = 1.5f;

	[SerializeField] float wobbleDuration = 0.5f;
	[SerializeField] float wobbleSpeed = 20f;
	[SerializeField] float wobbleAmount = 0.05f;

	Rigidbody2D rigidBody;
	Vector2 moveDirection;
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
	}

	void FixedUpdate() {
		rigidBody.velocity = moveDirection * moveSpeed;

		if (rigidBody.velocity.magnitude > 0f) {
			transform.localScale = Vector3.Lerp(originalScale * squeezeScale, originalScale * stretchScale, rigidBody.velocity.magnitude / moveSpeed);
		} else {
			transform.localScale = originalScale;
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (!isWobbling) {
			StartCoroutine(Wobble());
		}
	}

	// Prototyping
	void keyboardController() {
		if (Keyboard.current.wKey.isPressed) { }
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
