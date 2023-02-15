using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Health and attack properties
// Rename this class to Hero
public class Player : MonoBehaviour {

	// Migrate te JuiceEffects
	[SerializeField] SpriteRenderer flashEffect;

	[SerializeField] float movementSpeed = 4f;
	Vector2 movementDir;
	UnityAction movementAction;

	Rigidbody2D rigidBody;
	CircleCollider2D circleCollider;
	PlayerInput playerInput;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
		circleCollider = GetComponent<CircleCollider2D>();
		playerInput = GetComponent<PlayerInput>();
	}

	void Start() {
		movementAction = move;
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == Layers.enemy) {
			StartCoroutine(takeDamage(other, 0.5f));
			Events.getInstance().playerHit.Invoke(other);
		}
	}

	void Update() {
		movementDir = playerInput.getInput();
	}

	void FixedUpdate() {
		movementAction();
	}

	void move() {
		movementDir.Normalize();
		movementDir = checkForWallsAlongDirection(movementDir);
		rigidBody.MovePosition(rigidBody.position + movementDir * movementSpeed * Time.fixedDeltaTime);
		stride();
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

	// Strut, walk animation
	void stride() {
		const float angleRange = 16;
		const float freqMul = 4;

		float movement = Mathf.Clamp01(Mathf.Abs(movementDir.x) + Mathf.Abs(movementDir.y));
		rigidBody.MoveRotation(angleRange * Mathf.Sin(freqMul * movementSpeed * movement * Time.time));
	}

	IEnumerator knockback(Collision2D collision, float duration) {
		Vector2 initialPosition = rigidBody.position;
		float horizontalDistance = 2f;
		float horizontalTarget = horizontalDistance * Mathf.Sign(rigidBody.position.x - collision.rigidbody.position.x);

		for (float elapsedTime = 0; elapsedTime < duration; elapsedTime += Time.fixedDeltaTime) {
			Vector2 vertical = Vector2.up * Mathf.Sin(Mathf.PI * elapsedTime / duration);
			Vector2 horizontal = Vector2.right * Mathf.Lerp(0f, horizontalTarget, elapsedTime / duration);
			rigidBody.MovePosition(initialPosition + vertical + horizontal);
			yield return new WaitForFixedUpdate();
		}
	}

	IEnumerator takeDamage(Collision2D collision, float duration) {
		movementAction = delegate { };
		circleCollider.enabled = false;

		// Damage Effect
		StartCoroutine(flash(duration));
		StartCoroutine(wobble(duration));
		StartCoroutine(knockback(collision, duration));

		yield return new WaitForSeconds(duration);
		movementAction = move;

		// Invulnerability
		StartCoroutine(blink(0.5f));
		yield return new WaitForSeconds(0.5f);
		circleCollider.enabled = true;
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
}
