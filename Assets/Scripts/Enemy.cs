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
	Transform target;
	Vector3 movementDir;

	UnityAction movementAction;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void Start() {
		target = LevelManager.getInstance().getPlayer().transform;
		StartCoroutine(checkForTargetPeriodically(2f));
	}

	void FixedUpdate() {
		movementAction();
		stride();
	}

	void moveTowardsTarget() {
		movementDir = (target.position - transform.position).normalized;
		Vector3 towards = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.fixedDeltaTime);
		rigidBody.MovePosition(towards);
	}

	void stride() {
		const float angleRange = 16;
		const float freqMul = 4;

		float movement = Mathf.Clamp01(Mathf.Abs(movementDir.x) + Mathf.Abs(movementDir.y));
		rigidBody.MoveRotation(angleRange * Mathf.Sin(freqMul * movementSpeed * movement * Time.time));
	}

	IEnumerator checkForTargetPeriodically(float period) {
		// While player is alive/not beaten
		while (true) {
			if (Vector3.Distance(target.position, transform.position) <= 2) {
				movementAction = delegate { };
			} else {
				movementAction = moveTowardsTarget;
			}

			yield return new WaitForSeconds(period);
		}
	}
}
