using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IDamageable {
	void takeDamage(IDamager damager);
}

public class Hero : MonoBehaviour, IDamageable {
	[SerializeField] SpriteRenderer flashEffect;

	[SerializeField] int health;
	[SerializeField] int maxHealth;

	[SerializeField] float movementSpeed = 4f;
	Vector2 movementDir;
	UnityAction movementAction;

	Rigidbody2D rigidBody;
	CircleCollider2D circleCollider;
	HeroInput playerInput;

	// Remove this
	HealthBarUI healthBarUI;

	// MonoBehavior
	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		circleCollider = GetComponent<CircleCollider2D>();
		playerInput = GetComponent<HeroInput>();
		healthBarUI = FindObjectOfType<HealthBarUI>();
	}

	void Start() {
		movementAction = move;
	}

	void Update() {
		movementDir = playerInput.getInput();
	}

	void FixedUpdate() {
		movementAction();
	}


	// Movement
	void move() {
		moveAlongDirection(movementDir.normalized, movementSpeed);
		stride();
	}


	// Movement utilties
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


	// Reactions | Effects
	void stride() {
		const float angleRange = 16;
		const float freqMul = 4;

		float movement = Mathf.Clamp01(Mathf.Abs(movementDir.x) + Mathf.Abs(movementDir.y));
		rigidBody.MoveRotation(angleRange * Mathf.Sin(freqMul * movementSpeed * movement * Time.time));
	}

	IEnumerator knockback(Vector2 collisionPos, float duration) {
		Vector2 initialPosition = rigidBody.position;
		float horizontalDistance = 2f;
		float horizontalTarget = horizontalDistance * Mathf.Sign(rigidBody.position.x - collisionPos.x);

		for (float elapsedTime = 0; elapsedTime < duration; elapsedTime += Time.fixedDeltaTime) {
			Vector2 vertical = Vector2.up * Mathf.Sin(Mathf.PI * elapsedTime / duration);
			Vector2 horizontal = Vector2.right * Mathf.Lerp(0f, horizontalTarget, elapsedTime / duration);
			moveToPosition(initialPosition + vertical + horizontal);
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
	}


	// IDamageable and damage effects
	public void takeDamage(IDamager damager) {
		health -= damager.getDamage();
		healthBarUI.updateHealthBar(health, maxHealth);

		if (health > 0)
			StartCoroutine(onHeroDamage(damager));
		else
			StartCoroutine(onHeroDie());
	}

	IEnumerator onHeroDamage(IDamager damager) {
		LevelManager.getInstance().getSoundManager().getPlayerSound().playDamage();
		movementAction = delegate { };
		circleCollider.enabled = false;
		yield return damageEffects(0.5f, damager.getPosition());
		movementAction = move;
		yield return invulnerability(0.5f);
		circleCollider.enabled = true;
	}

	IEnumerator onHeroDie() {
		LevelManager.getInstance().getSoundManager().getPlayerSound().playDie();
		movementAction = delegate { };
		circleCollider.enabled = false;
		yield return die(0.5f);
		Events.getInstance().heroBeaten.Invoke();
	}

	IEnumerator damageEffects(float duration, Vector2 position) {
		StartCoroutine(flash(duration));
		StartCoroutine(wobble(duration));
		StartCoroutine(knockback(position, duration));
		yield return new WaitForSeconds(duration);
	}

	IEnumerator invulnerability(float duration) {
		StartCoroutine(blink(duration));
		yield return new WaitForSeconds(duration);
	}


	// Juice effects
	IEnumerator flash(float duration) {
		flashEffect.enabled = !flashEffect.enabled;
		yield return new WaitForSeconds(duration);
		flashEffect.enabled = !flashEffect.enabled;
	}

	IEnumerator blink(float duration) {
		for (float elapsedTime = 0; elapsedTime < duration || flashEffect.enabled; elapsedTime += Time.deltaTime) {
			flashEffect.enabled = !flashEffect.enabled;
			yield return new WaitForSeconds(Time.deltaTime);
		}
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


	// Getters
	public int getHealth() { return health; }
	public int getMaxHealth() { return maxHealth; }
}
