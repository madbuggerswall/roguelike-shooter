using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Basic, Fast, Tank, Ranged, Boss
// TODO: Jelly, Ghost, Brute, Wizard behaviors
// MAYBE: Enemy states

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public abstract class Enemy : MonoBehaviour, IPoolable {
	[SerializeField] GameObject flashEffect;
	[SerializeField] GameObject exclamation;

	[SerializeField] int health;
	[SerializeField] int damage;
	[SerializeField] float attackRadius;

	[SerializeField] float movementSpeed;
	[SerializeField] Vector2 movementDir;

	Rigidbody2D rigidBody;
	CircleCollider2D circleCollider;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		circleCollider = GetComponent<CircleCollider2D>();

		rigidBody.isKinematic = true;
		rigidBody.useFullKinematicContacts = true;
	}

	void OnEnable() {
		StartCoroutine(chaseAndAttack(2f));
	}

	// TODO
	void IPoolable.reset() { }

	// TODO
	void IPoolable.returnToPool() { }

	// TODO This deforms the enemy and causes weird behavior when player attack period is low.
	public void takeDamage(int damage) {
		if (health <= 0)
			return;

		health -= damage;
		StartCoroutine(flash(0.5f));
		StartCoroutine(wobble(0.5f));
		StartCoroutine(knockback(0.5f));
		// updateHealthBar();

		if (health <= 0) {
			// Stop and die
			stride(0);
			StopAllCoroutines();
			StartCoroutine(die(0.5f));
			Events.getInstance().enemyBeaten.Invoke(getEnemyType());
		}
	}

	IEnumerator knockback(float duration) {
		Vector2 initialPosition = rigidBody.position;
		for (float elapsedTime = 0; elapsedTime < duration; elapsedTime += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + Vector2.up * Mathf.Sin(Mathf.PI / duration * elapsedTime));
			yield return new WaitForFixedUpdate();
		}
	}

	void moveAlongDirection(Vector2 direction, float speed) {
		rigidBody.MovePosition(rigidBody.position + direction * speed * Time.fixedDeltaTime);
	}

	void moveTowardsTarget(Transform target) {
		Vector2 towards = Vector2.MoveTowards(rigidBody.position, target.position, movementSpeed * Time.fixedDeltaTime);
		rigidBody.MovePosition(towards);
	}

	IEnumerator calculateDirectionPeriodically(Transform target, float period) {
		System.Func<Vector2, Vector2, float, bool> inRadius = (a, b, r) => (a - b).SqrMagnitude() <= r * r;

		// OverlapCircle boilerplate
		float radius = 1f;
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
		Collider2D[] enemiesAround = new Collider2D[9];

		// TODO radius plugged below is wrong
		while (!inRadius(rigidBody.position, target.position, radius)) {
			movementDir = ((Vector2) target.position - rigidBody.position).normalized;

			// Avoid overlap
			int enemyCount = Physics2D.OverlapCircle(rigidBody.position, radius, contactFilter, enemiesAround);
			movementDir = movementDir + getPushDirection(enemiesAround, enemyCount).normalized;

			yield return new WaitForSeconds(period);
		}
	}

	Vector2 getPushDirection(Collider2D[] enemiesAround, int enemyCount) {
		Vector2 pushDirection = Vector2.zero;
		for (int i = 0; i < enemyCount; i++) {
			// Don't repel self
			if (enemiesAround[i] == circleCollider)
				continue;

			float distance = Vector2.Distance(enemiesAround[i].transform.position, transform.position);
			float pushStrength = 1f / (1f + distance);
			Vector2 pushContribution = -(enemiesAround[i].transform.position - transform.position).normalized;

			pushDirection += pushContribution * pushStrength;
		}
		return pushDirection;
	}

	void stride(float movementSpeed) {
		const float angleRange = 16;
		const float freqMul = 4;
		rigidBody.MoveRotation(angleRange * Mathf.Sin(freqMul * movementSpeed * Time.time));
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
		circleCollider.enabled = false;

		Vector2 initialPosition = rigidBody.position;
		for (float elapsedTime = 0; elapsedTime < duration; elapsedTime += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + Vector2.up * Mathf.Sin(Mathf.PI / duration * elapsedTime));
			yield return new WaitForFixedUpdate();
		}

		exclamation.SetActive(false);
		circleCollider.enabled = true;
	}

	IEnumerator dash(Transform target, float duration) {
		float distance = 4;
		float speed = distance / duration;
		float drag = speed;

		Vector2 direction = ((Vector2) target.position - rigidBody.position).normalized;
		Vector2 initialPosition = rigidBody.position;
		Vector2 targetPosition = rigidBody.position + direction * distance;

		for (float elapsedTime = 0; elapsedTime <= duration; elapsedTime += Time.fixedDeltaTime) {

			float t = Tween.easeOutSine(elapsedTime, duration);
			Vector2 position = Vector2.Lerp(initialPosition, targetPosition, t);
			rigidBody.MovePosition(position);
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator moveTowards(Transform target, float radius) {
		System.Func<Vector2, Vector2, float, bool> inRadius = (a, b, r) => (a - b).SqrMagnitude() <= r * r;

		while (!inRadius(rigidBody.position, target.position, radius)) {
			moveAlongDirection(movementDir, movementSpeed);
			stride(movementSpeed);
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator chaseAndAttack(float radius) {
		Transform target = LevelManager.getInstance().getPlayer().transform;

		// While player is alive/not beaten
		while (true) {
			// Chase
			StartCoroutine(calculateDirectionPeriodically(target, 0.1f));
			yield return moveTowards(target, radius);
			stride(0);

			// Notice
			Events.getInstance().playerNoticed.Invoke();
			yield return notice(0.5f);
			yield return new WaitForSeconds(0.5f);

			// Attack
			yield return dash(target, 0.5f);
			yield return new WaitForSeconds(0.5f);
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

	protected abstract EnemyType getEnemyType();
}
