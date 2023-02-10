using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Basic, Fast, Tank, Ranged, Boss

public class Enemy : MonoBehaviour {
	[SerializeField] GameObject flashEffect;
	[SerializeField] GameObject exclamation;

	[SerializeField] int health;
	[SerializeField] int damage;

	[SerializeField] float movementSpeed;

	Rigidbody2D rigidBody;
	BoxCollider2D boxCollider;
	UnityAction movementAction;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		boxCollider = GetComponent<BoxCollider2D>();
	}

	void Start() {
		StartCoroutine(checkForTargetPeriodically(2f, 1f));
	}

	void FixedUpdate() {
		movementAction();
	}

	// Take damage and notify evetns if beaten 
	// Stop moving when taken damage
	public void takeDamage(int damage) {
		health -= damage;
		StartCoroutine(flash(0.5f));
		StartCoroutine(wobble());
		Events.getInstance().enemyHit.Invoke();
		// updateHealthBar();

		if (health <= 0) {
			// Stop and die
			movementAction = delegate { };
			stride(0);
			StartCoroutine(die(0.5f));
		}
	}

	IEnumerator die(float duration) {

		Vector2 initialPosition = rigidBody.position;
		for (float elapsedTime = 0; elapsedTime < duration; elapsedTime += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + Vector2.up * Mathf.Sin(Mathf.PI / duration * elapsedTime));
			rigidBody.MoveRotation(elapsedTime / duration * 90);
			yield return new WaitForFixedUpdate();
		}

		// Events.getInstance().enemyBeaten.Invoke(getEnemyType());
		gameObject.SetActive(false);
	}

	IEnumerator notice(float duration) {
		exclamation.SetActive(true);
		boxCollider.enabled = false;

		Vector2 initialPosition = rigidBody.position;
		for (float elapsedTime = 0; elapsedTime < duration; elapsedTime += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + Vector2.up * Mathf.Sin(Mathf.PI / duration * elapsedTime));
			yield return new WaitForFixedUpdate();
		}

		exclamation.SetActive(false);
		boxCollider.enabled = true;
	}

	void moveTowardsTarget(Vector3 targetPosition) {
		Vector3 towards = Vector3.MoveTowards(rigidBody.position, targetPosition, movementSpeed * Time.fixedDeltaTime);
		rigidBody.MovePosition(towards);
	}

	void stride(float movementSpeed) {
		const float angleRange = 16;
		const float freqMul = 4;
		rigidBody.MoveRotation(angleRange * Mathf.Sin(freqMul * movementSpeed * Time.time));
	}

	IEnumerator dash(Transform target) {
		float speed = 8;
		float drag = 8;
		float distance = 4;

		Vector2 direction = ((Vector2) target.position - rigidBody.position).normalized;
		Vector2 targetPosition = rigidBody.position + direction * distance;

		while (rigidBody.position != targetPosition) {
			Vector3 towards = Vector3.MoveTowards(rigidBody.position, targetPosition, speed * Time.fixedDeltaTime);
			rigidBody.MovePosition(towards);
			speed = Mathf.Clamp(speed - drag * Time.deltaTime, 1f, 8f);
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator checkForTargetPeriodically(float radius, float period) {
		Transform target = LevelManager.getInstance().getPlayer().transform;

		// While player is alive/not beaten
		while (true) {
			if (Vector3.Distance(target.position, transform.position) <= radius) {
				// Stop
				movementAction = delegate { };
				stride(0);

				// Notice
				Events.getInstance().playerNoticed.Invoke();
				yield return notice(0.5f);
				yield return new WaitForSeconds(0.5f);

				// Attack
				yield return dash(target);
				yield return new WaitForSeconds(0.5f);
			} else {
				movementAction = delegate {
					moveTowardsTarget(target.position);
					stride(movementSpeed);
				};
			}

			yield return new WaitForSeconds(period);
		}
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
