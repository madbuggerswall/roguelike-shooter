using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Health and attack properties
public class Player : MonoBehaviour {

	// Migrate te JuiceEffects
	[SerializeField] GameObject flashEffect;

	[SerializeField] float wobbleDuration = 0.5f;
	[SerializeField] float wobbleSpeed = 20f;
	[SerializeField] float wobbleAmount = 0.05f;

	void Awake() {
	}

	void OnCollisionEnter2D(Collision2D other) {
		StartCoroutine(flash(0.5f));
		StartCoroutine(wobble());
		Events.getInstance().playerHit.Invoke();
	}

	IEnumerator flash(float duration) {
		flashEffect.SetActive(!flashEffect.activeInHierarchy);
		yield return new WaitForSeconds(duration);
		flashEffect.SetActive(!flashEffect.activeInHierarchy);
	}

	IEnumerator wobble() {
		float elapsedTime = 0f;
		Vector2 initialScale = transform.localScale;

		while (elapsedTime < wobbleDuration) {
			float scale = Mathf.Sin(elapsedTime * wobbleSpeed) * wobbleAmount;
			transform.localScale = initialScale + Vector2.one * scale;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		
		transform.localScale = initialScale;
	}
}
