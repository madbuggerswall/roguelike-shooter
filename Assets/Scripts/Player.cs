using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Health and attack properties
// TODO Invulnerability frames
// TODO Players and enemies move through walls Fix it 
// TODO Player should have a knockback

public class Player : MonoBehaviour {

	// Migrate te JuiceEffects
	[SerializeField] GameObject flashEffect;

	void Awake() {
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == Layers.enemy) {
			StartCoroutine(flash(0.5f));
			StartCoroutine(wobble(0.5f));
			StartCoroutine(knockback(other, 0.5f));
			Events.getInstance().playerHit.Invoke();
		}
	}

	IEnumerator knockback(Collision2D collision, float duration) {
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		Vector2 initialPosition = rigidBody.position;
		for (float elapsedTime = 0; elapsedTime < duration; elapsedTime += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + Vector2.up * Mathf.Sin(Mathf.PI / duration * elapsedTime));
			yield return new WaitForFixedUpdate();
		}
	}

	// Juice effects
	IEnumerator flash(float duration) {
		flashEffect.SetActive(!flashEffect.activeInHierarchy);
		yield return new WaitForSeconds(duration);
		flashEffect.SetActive(!flashEffect.activeInHierarchy);
	}

	IEnumerator wobble(float duration) {
		float speed = 40f;
		float amount = 0.1f;

		float elapsedTime = 0f;
		Vector2 initialScale = transform.localScale;

		while (elapsedTime < duration) {
			float scale = Mathf.Sin(elapsedTime * speed) * amount;
			transform.localScale = initialScale + Vector2.one * scale;
			elapsedTime += Time.deltaTime;
			yield return null;
		}

		transform.localScale = initialScale;
	}
}
