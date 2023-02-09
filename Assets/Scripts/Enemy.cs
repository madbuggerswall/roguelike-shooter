using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Basic, Fast, Tank, Ranged, Boss

public class Enemy : MonoBehaviour {
	[SerializeField] float movementSpeed;

	[SerializeField] int health;
	[SerializeField] int damage;

	Rigidbody2D rigidBody;
	UnityAction movementAction;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void Start() {
		StartCoroutine(checkForTargetPeriodically(2f, 1f));
	}

	void FixedUpdate() {
		movementAction();
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

	void squeezeAndStretch(Vector2 direction) {
		float squeezeMul = 0.1f;
		float stretchMul = 0.1f;
		Vector2 originalScale = Vector2.one;

		(float x, float y) scale;
		scale.x = stretchMul * Mathf.Abs(direction.x) - squeezeMul * Mathf.Abs(direction.y);
		scale.y = stretchMul * Mathf.Abs(direction.y) - squeezeMul * Mathf.Abs(direction.x);
		transform.localScale = originalScale + new Vector2(scale.x, scale.y);
	}

	IEnumerator dash(Transform target, float cooldownDuration) {
		float speed = 8;
		float drag = 8;
		float distance = 4;

		Vector2 direction = ((Vector2) target.position - rigidBody.position).normalized;
		Vector2 initialPosition = rigidBody.position;
		Vector2 targetPosition = rigidBody.position + direction * distance;
		
		for (float t = 0; t < 1; t += speed / distance * Time.fixedDeltaTime) {
			Vector2 towards = Vector2.Lerp(initialPosition, targetPosition, t);
			rigidBody.MovePosition(towards);
			speed -= drag * Time.fixedDeltaTime;
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator checkForTargetPeriodically(float radius, float period) {
		Transform target = LevelManager.getInstance().getPlayer().transform;

		// While player is alive/not beaten
		while (true) {
			if (Vector3.Distance(target.position, transform.position) <= radius) {
				movementAction = delegate { };
				stride(0);
				StartCoroutine(dash(target, 1f));
			} else {
				movementAction = delegate {
					moveTowardsTarget(target.position);
					stride(movementSpeed);
				};
			}

			yield return new WaitForSeconds(period);
		}
	}

	// Vector3 getAvailablePositionAroundTarget() {
	// Vector2[] directions = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
	// Vector3 targetPosition;

	// foreach (Vector2 direction in directions) {

	// }
	// }

	// IEnumerator dash(Transform target) {
	// }
}
