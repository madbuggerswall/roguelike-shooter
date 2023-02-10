using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Health and attack properties
public class Player : MonoBehaviour {

	// Migrate te JuiceEffects
	[SerializeField] GameObject flashEffect;

	void Awake() {
	}

	void OnCollisionEnter2D(Collision2D other) {
		StartCoroutine(flash(0.5f));
		StartCoroutine(wobble());
		Events.getInstance().playerHit.Invoke();
	}

	// Juice effects
	IEnumerator flash(float duration) {
		flashEffect.SetActive(!flashEffect.activeInHierarchy);
		yield return new WaitForSeconds(duration);
		flashEffect.SetActive(!flashEffect.activeInHierarchy);
	}

	IEnumerator wobble() {
		float duration = 0.5f;
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
