using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamager {
	void dealDamage(IDamageable damageable);
	int getDamage();
	float getDuration();
	Vector2 getPosition();
}

public abstract class Projectile : MonoBehaviour, IPoolable, IDamager {
	[SerializeField] float speed = 8f;
	[SerializeField] float offsetAngle;

	int damage = 8;
	float damageDuration = 0.3f;

	Vector2 direction;
	Rigidbody2D rigidBody;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	// TODO: Break on walls too, IDamager calls damage operations
	void OnCollisionEnter2D(Collision2D other) {
		IDamageable damageable;
		if (other.gameObject.TryGetComponent<IDamageable>(out damageable))
			dealDamage(damageable);

		LevelManager.getInstance().getParticles().spawnParticles(this, rigidBody.position);
		LevelManager.getInstance().getCameraImpulse().impulse(other);
		returnToPool();
	}

	void FixedUpdate() {
		moveAlongDirection(direction);
		lookAtDirection(direction);
	}

	public void throwAtTarget(Transform target, int damage, float speed) {
		this.direction = (target.transform.position - transform.position).normalized;
		this.damage = damage;
		this.speed = speed;
	}

	void moveAlongDirection(Vector2 direction) {
		rigidBody.MovePosition(rigidBody.position + direction * speed * Time.fixedDeltaTime);
	}

	void lookAtDirection(Vector2 direction) {
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - offsetAngle;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	// IDamager
	public void dealDamage(IDamageable damageable) { damageable.takeDamage(this); }
	public int getDamage() { return damage; }
	public float getDuration() { return damageDuration; }
	public Vector2 getPosition() { return transform.position; }

	// IPoolable
	public void reset() { }
	public void returnToPool() { gameObject.SetActive(false); }
}
