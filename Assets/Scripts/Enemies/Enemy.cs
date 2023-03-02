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
public abstract class Enemy : MonoBehaviour, IPoolable, IDamageable, IDamager {
	[SerializeField] SpriteRenderer flashEffect;
	[SerializeField] SpriteRenderer exclamation;
	[SerializeField] SpriteRenderer healthBar;

	[SerializeField] protected int maxHealth;
	[SerializeField] protected int health;
	[SerializeField] protected int damage;
	[SerializeField] protected float damageDuration;
	[SerializeField] protected float attackRadius;
	[SerializeField] protected float movementSpeed;

	Vector2 movementDir;
	Vector2 initialScale;

	protected Rigidbody2D rigidBody;
	CircleCollider2D circleCollider;

	// MonoBehavior
	protected virtual void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		circleCollider = GetComponent<CircleCollider2D>();

		rigidBody.isKinematic = true;
		rigidBody.useFullKinematicContacts = true;
		initialScale = transform.localScale;
	}

	void OnEnable() {
		StartCoroutine(chaseAndAttack());
	}

	void OnDisable() {
		reset();
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == Layers.hero) {
			other.gameObject.GetComponent<IDamageable>().takeDamage(this);
			// LevelManager.getInstance().getParticles().spawnParticles(this, rigidBody.position);
			LevelManager.getInstance().getCameraImpulse().impulse(other);
		}
	}

	// Behavior | Strategy
	protected abstract IEnumerator chaseAndAttack();


	// Movement
	protected virtual IEnumerator chase(Transform target) {
		Coroutine directionRoutine = StartCoroutine(calculateDirectionPeriodically(target, 0.2f));
		Coroutine strideRoutine = StartCoroutine(stride(movementSpeed));

		yield return chaseUntilTargetInRadius(target);

		StopCoroutine(directionRoutine);
		StopCoroutine(strideRoutine);
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

		returnToPool();
	}

	IEnumerator stride(float movementSpeed) {
		const float angleRange = 16;
		const float freqMul = 4;

		while (true) {
			rigidBody.MoveRotation(angleRange * Mathf.Sin(freqMul * movementSpeed * Time.fixedTime));
			yield return new WaitForFixedUpdate();
		}
	}


	// Movement utilities
	protected IEnumerator calculateDirectionPeriodically(Transform target, float period) {
		float radius = 1f;
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(Layers.enemyMask);
		List<Collider2D> enemiesAround = new List<Collider2D>();

		while (true) {
			Physics2D.OverlapCircle(rigidBody.position, radius, contactFilter, enemiesAround);
			movementDir = ((Vector2) target.position - rigidBody.position).normalized;
			movementDir += getPushDirection(enemiesAround).normalized;

			yield return new WaitForSeconds(period);
		}
	}

	protected IEnumerator chaseUntilTargetInRadius(Transform target) {
		System.Func<Vector2, Vector2, float, bool> inRadius = (a, b, r) => (a - b).SqrMagnitude() <= r * r;
		while (!inRadius(target.position, rigidBody.position, attackRadius)) {
			moveAlongDirection(movementDir, movementSpeed);
			yield return new WaitForFixedUpdate();
		}
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


	// Damage Operations, takeDamage should be IDamagable.takeDamage()
	public void takeDamage(IDamager damager) {
		const float damageCooldown = 0.1f;
		health -= damager.getDamage();
		updateHealthBar(health, maxHealth);

		if (health > 0) {
			StopAllCoroutines();
			StartCoroutine(damageEffects(damager.getDuration(), damageCooldown));
		} else {
			circleCollider.enabled = false;
			StopAllCoroutines();
			StartCoroutine(die(0.5f));
			Events.getInstance().enemyBeaten.Invoke(this);
		}
	}

	IEnumerator damageEffects(float duration, float cooldown) {
		exclamation.enabled = false;

		StartCoroutine(flash(duration));
		StartCoroutine(wobble(duration));
		StartCoroutine(knockback(duration));
		yield return new WaitForSeconds(duration + cooldown);
		StartCoroutine(chaseAndAttack());
	}

	void updateHealthBar(int health, int maxHealth) {
		float healthBarWidth = Mathf.Clamp((float) health / maxHealth, 0, maxHealth);
		healthBar.size = new Vector2(healthBarWidth, 0.125f);
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


	// IDamager
	public void dealDamage(IDamageable damageable) { damageable.takeDamage(this); }
	public int getDamage() { return damage; }
	public float getDuration() { return damageDuration; }
	public Vector2 getPosition() { return transform.position; }

	// IPoolable
	public void reset() {
		health = maxHealth;
		flashEffect.enabled = false;
		exclamation.enabled = false;
		circleCollider.enabled = true;
		transform.localScale = initialScale;
		updateHealthBar(health, maxHealth);
	}

	public void returnToPool() {
		gameObject.SetActive(false);
	}
}