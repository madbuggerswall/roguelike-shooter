using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Basic, Fast, Tank, Ranged, Boss
// TODO: Jelly, Ghost, Brute, Wizard behaviors
// MAYBE: Enemy states

// Jellies melee	speed: medium | damage: medium
// Ghosts melee 	speed: high | damage: low
// Brute dash			speed: low | damage: high
// Wizard shoot		speed: low | damage: medium
// Boss?

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public abstract class Enemy : MonoBehaviour, IPoolable {
	[SerializeField] SpriteRenderer flashEffect;
	[SerializeField] SpriteRenderer exclamation;
	[SerializeField] SpriteRenderer healthBar;

	[SerializeField] int maxHealth;
	[SerializeField] int health;
	[SerializeField] int damage;
	[SerializeField] float attackRadius;

	[SerializeField] float movementSpeed;
	[SerializeField] Vector2 movementDir;

	Vector2 initialScale;

	Rigidbody2D rigidBody;
	CircleCollider2D circleCollider;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		circleCollider = GetComponent<CircleCollider2D>();

		rigidBody.isKinematic = true;
		rigidBody.useFullKinematicContacts = true;
		initialScale = transform.localScale;
	}

	void OnEnable() {
		reset();
	}

	// IPoolable
	public void reset() {
		health = maxHealth;
		flashEffect.enabled = false;
		exclamation.enabled = false;
		circleCollider.enabled = true;
		transform.localScale = initialScale;
		updateHealthBar(health, maxHealth);
		StartCoroutine(chaseAndAttack());
	}

	// IPoolable
	public void returnToPool() {
		gameObject.SetActive(false);
	}

	// Behavior/Strategy
	protected abstract IEnumerator chaseAndAttack();


	// TODO Damage effects duration determined by damage or projectile (Axe: 0.5f, Sword 0.25f, Arrow 0.1f)
	public void takeDamage(int damage) {
		health -= damage;
		updateHealthBar(health, maxHealth);
		StopAllCoroutines();
		StartCoroutine(damageEffects(0.3f, 0.2f));

		if (health <= 0) {
			// Stop and die
			circleCollider.enabled = false;
			StopAllCoroutines();
			StartCoroutine(die(0.5f));
			Events.getInstance().enemyBeaten.Invoke(this, transform.position);
		}
	}

	IEnumerator damageEffects(float duration, float cooldown) {
		stride(0);
		exclamation.enabled = false;

		StartCoroutine(flash(duration));
		StartCoroutine(wobble(duration));
		StartCoroutine(knockback(duration));
		yield return new WaitForSeconds(duration + cooldown);
		StartCoroutine(chaseAndAttack());
	}

	// Scale the health bar to visualize health
	void updateHealthBar(int health, int maxHealth) {
		float healthBarWidth = Mathf.Clamp((float) health / maxHealth, 0, maxHealth);
		healthBar.size = new Vector2(healthBarWidth, 0.125f);
	}

	// Movement utilities
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

	void moveAlongDirection(Vector2 direction, float speed) {
		direction = checkForWallsAlongDirection(direction);
		rigidBody.MovePosition(rigidBody.position + direction * speed * Time.fixedDeltaTime);
	}

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

	Vector2 getPushDirection(List<Collider2D> enemiesAround) {
		Vector2 pushDirection = Vector2.zero;

		foreach (Collider2D enemy in enemiesAround) {
			if (enemy == circleCollider)
				continue;

			float distance = Vector2.Distance(enemy.transform.position, transform.position);
			float pushStrength = 1f / (1f + distance);
			Vector2 pushContribution = -(enemy.transform.position - transform.position).normalized;
			pushDirection += pushContribution * pushStrength;
		}

		return pushDirection;
	}

	IEnumerator calculateDirectionPeriodically(Transform target, float period) {
		// OverlapCircle boilerplate
		float radius = 1f;
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(Layers.enemyMask);
		List<Collider2D> enemiesAround = new List<Collider2D>();

		while (true) {
			movementDir = ((Vector2) target.position - rigidBody.position).normalized;

			// Avoid overlap
			Physics2D.OverlapCircle(rigidBody.position, radius, contactFilter, enemiesAround);
			movementDir = movementDir + getPushDirection(enemiesAround).normalized;

			yield return new WaitForSeconds(period);
		}
	}


	// Behaviors
	protected IEnumerator chase(Transform target) {
		System.Func<Vector2, Vector2, float, bool> inRadius = (a, b, r) => (a - b).SqrMagnitude() <= r * r;
		Coroutine calculateDirection = StartCoroutine(calculateDirectionPeriodically(target, 0.1f));

		while (!inRadius(target.position, rigidBody.position, attackRadius)) {
			moveAlongDirection(movementDir, movementSpeed);
			stride(movementSpeed);
			yield return new WaitForFixedUpdate();
		}

		StopCoroutine(calculateDirection);
		stride(0);
	}

	// Attack
	protected IEnumerator dash(Transform target, float duration, float distance) {
		float speed = distance / duration;
		float drag = speed;

		Vector2 direction = ((Vector2) target.position - rigidBody.position).normalized;
		Vector2 initialPosition = rigidBody.position;
		Vector2 targetPosition = rigidBody.position + direction * distance;

		for (float time = 0; time <= duration; time += Time.fixedDeltaTime) {
			Vector2 position = Vector2.Lerp(initialPosition, targetPosition, Tween.easeOutCubic(time, duration));
			moveToPosition(position);
			yield return new WaitForFixedUpdate();
		}
	}

	protected IEnumerator melee(Transform target, float duration, float distance) {
		Vector2 initialPosition = rigidBody.position;
		Vector2 direction = (target.position - transform.position).normalized;

		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			moveToPosition(initialPosition + distance * direction * Mathf.Sin(Mathf.PI * Tween.easeOutQuad(time, duration)));
			yield return new WaitForFixedUpdate();
		}
	}

	// Reactions | Effects
	IEnumerator knockback(float duration) {
		Vector2 initialPosition = rigidBody.position;
		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			moveToPosition(initialPosition + Vector2.up * duration * Mathf.Sin(Mathf.PI * time / duration));
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator die(float duration) {
		Vector2 initialPosition = rigidBody.position;
		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			rigidBody.MovePosition(initialPosition + Vector2.up * Mathf.Sin(Mathf.PI * time / duration));
			rigidBody.MoveRotation(time / duration * 90);
			yield return new WaitForFixedUpdate();
		}

		// Events.getInstance().enemyBeaten.Invoke(getEnemyType());
		returnToPool();
	}

	protected IEnumerator notice(float duration) {
		LevelManager.getInstance().getSoundManager().playHeroNoticed();

		float jumpHeight = duration;
		exclamation.enabled = true;

		Vector2 initialPosition = rigidBody.position;
		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			moveToPosition(initialPosition + Vector2.up * jumpHeight * Mathf.Sin(Mathf.PI * time / duration));
			yield return new WaitForFixedUpdate();
		}

		exclamation.enabled = false;
	}

	// Juice effects
	IEnumerator flash(float duration) {
		flashEffect.enabled = true;
		yield return new WaitForSeconds(duration);
		flashEffect.enabled = false;
	}

	IEnumerator wobble(float duration) {
		float frequency = 10f;
		float amount = 0.1f;

		for (float time = 0; time < duration; time += Time.fixedDeltaTime) {
			float scale = Mathf.Sin(Mathf.PI * frequency * Tween.easeOutSine(time, duration)) * amount;
			transform.localScale = initialScale + Vector2.one * scale;
			yield return new WaitForFixedUpdate();
		}

		transform.localScale = initialScale;
	}

	// Getters
	public int getDamage() { return damage; }
}
