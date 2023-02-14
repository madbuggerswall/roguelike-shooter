using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Basic, Fast, Tank, Ranged, Boss
// TODO: Jelly, Ghost, Brute, Wizard behaviors
// MAYBE: Enemy states

// Jellies dash
// Ghosts don't

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public abstract class Enemy : MonoBehaviour, IPoolable {
	[SerializeField] SpriteRenderer flashEffect;
	[SerializeField] SpriteRenderer exclamation;

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

	Vector2 checkForWallsAlongDirection(Vector2 direction) {
		Vector2 verticalPosition = rigidBody.position + Vector2.up * Mathf.Sign(direction.y);
		Vector2 horizontalPosition = rigidBody.position + Vector2.right * Mathf.Sign(direction.x);

		Collider2D vertical = Physics2D.OverlapPoint(verticalPosition, Layers.wallMask);
		Collider2D horizontal = Physics2D.OverlapPoint(horizontalPosition, Layers.wallMask);

		if (vertical is not null)
			direction.y = 0;
		if (horizontal is not null)
			direction.x = 0;

		return direction;
	}

	// Use this for a collision aware movement
	void moveAlongDirection(Vector2 direction, float speed) {
		direction = checkForWallsAlongDirection(direction);
		rigidBody.MovePosition(rigidBody.position + direction * speed * Time.fixedDeltaTime);
	}

	// Use this for a collision aware movement
	void moveToPosition(Vector2 position) {
		Vector2 direction = position - rigidBody.position;
		direction = checkForWallsAlongDirection(direction);
		rigidBody.MovePosition(rigidBody.position + direction);
	}

	void stride(float movementSpeed) {
		const float angleRange = 16;
		const float freqMul = 4;

		float movement = Mathf.Clamp01(Mathf.Abs(movementDir.x) + Mathf.Abs(movementDir.y));
		rigidBody.MoveRotation(angleRange * Mathf.Sin(freqMul * movementSpeed * movement * Time.time));
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

	IEnumerator calculateDirectionPeriodically(Transform target, float period) {
		System.Func<Vector2, Vector2, float, bool> inRadius = (a, b, r) => (a - b).SqrMagnitude() <= r * r;

		// OverlapCircle boilerplate
		float radius = 1f;
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
		Collider2D[] enemiesAround = new Collider2D[9];

		while (!inRadius(rigidBody.position, target.position, attackRadius)) {
			movementDir = ((Vector2) target.position - rigidBody.position).normalized;

			// Avoid overlap
			int enemyCount = Physics2D.OverlapCircle(rigidBody.position, radius, contactFilter, enemiesAround);
			movementDir = movementDir + getPushDirection(enemiesAround, enemyCount).normalized;

			yield return new WaitForSeconds(period);
		}
	}

	IEnumerator moveTowards(Transform target) {
		System.Func<Vector2, Vector2, float, bool> inRadius = (a, b, r) => (a - b).SqrMagnitude() <= r * r;

		while (!inRadius(rigidBody.position, target.position, attackRadius)) {
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
			yield return moveTowards(target);
			stride(0);

			// Notice
			Events.getInstance().playerNoticed.Invoke();
			yield return notice(0.2f);
			// yield return new WaitForSeconds(0.5f);

			// // Attack
			// yield return melee(target, 0.5f, 1f);
			yield return melee(target, 0.5f);
			yield return new WaitForSeconds(0.5f);
		}
	}

	IEnumerator dash(Transform target, float duration) {
		float distance = 4;
		float speed = distance / duration;
		float drag = speed;

		Vector2 direction = ((Vector2) target.position - rigidBody.position).normalized;
		Vector2 initialPosition = rigidBody.position;
		Vector2 targetPosition = rigidBody.position + direction * distance;

		for (float time = 0; time <= duration; time += Time.fixedDeltaTime) {
			Vector2 position = Vector2.Lerp(initialPosition, targetPosition, Tween.easeOutSine(time, duration));
			moveToPosition(position);
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator melee(Transform target, float duration) {
		Vector2 initialPosition = rigidBody.position;
		Vector2 direction = (target.position - transform.position).normalized;

		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + direction * Mathf.Sin(Mathf.PI * Tween.easeOutQuad(time, duration)));
			yield return new WaitForFixedUpdate();
		}
	}

	// Reactions
	IEnumerator knockback(float duration) {
		Vector2 initialPosition = rigidBody.position;
		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + Vector2.up * Mathf.Sin(Mathf.PI / duration * time));
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator die(float duration) {
		Vector2 initialPosition = rigidBody.position;
		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + Vector2.up * Mathf.Sin(Mathf.PI / duration * time));
			rigidBody.MoveRotation(time / duration * 90);
			yield return new WaitForFixedUpdate();
		}

		// Events.getInstance().enemyBeaten.Invoke(getEnemyType());
		gameObject.SetActive(false);
	}

	IEnumerator notice(float duration) {
		float jumpHeight = duration;

		exclamation.enabled = true;
		circleCollider.enabled = false;

		Vector2 initialPosition = rigidBody.position;
		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + Vector2.up * jumpHeight * Mathf.Sin(Mathf.PI / duration * time));
			yield return new WaitForFixedUpdate();
		}

		exclamation.enabled = false;
		circleCollider.enabled = true;
	}

	// Juice effects
	IEnumerator flash(float duration) {
		flashEffect.enabled = !flashEffect.enabled;
		yield return new WaitForSeconds(duration);
		flashEffect.enabled = !flashEffect.enabled;
	}

	IEnumerator wobble(float duration) {
		float frequency = 20f;
		float amount = 0.1f;

		Vector2 initialScale = transform.localScale;

		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			float scale = Mathf.Sin(Mathf.PI * frequency * time / duration) * amount;
			transform.localScale = initialScale + Vector2.one * scale;
			yield return new WaitForFixedUpdate();
		}
		
		transform.localScale = initialScale;
	}

	// Abstract
	protected abstract EnemyType getEnemyType();
}
