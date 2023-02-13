using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IPoolable
public class Projectile : MonoBehaviour {

	[SerializeField] int damage = 10;
	[SerializeField] float speed = 8f;
	[SerializeField] Vector2 direction;

	[SerializeField] float offsetAngle;

	Rigidbody2D rigidBody;

	void Awake() {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			other.gameObject.GetComponent<Enemy>().takeDamage(damage);
			gameObject.SetActive(false);
			
			Events.getInstance().enemyHit.Invoke(other);
		}
	}

	void FixedUpdate() {
		moveAlongDirection(direction);
		lookAtDirection(direction);
	}

	// Transform based movement
	void moveAlongDirection(Vector2 direction) {
		rigidBody.MovePosition(rigidBody.position + direction * speed * Time.fixedDeltaTime);
	}

	// Atan solution
	void lookAtDirection(Vector2 direction) {
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + offsetAngle;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	public void throwAtTarget(Transform target, int damage) {
		this.damage = damage;
		this.direction = (target.transform.position - transform.position).normalized;
	}

	// Getters
	public int getDamage() { return damage; }
}
