using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IPoolable {
	[SerializeField] protected float attackRadius;
	[SerializeField] protected float attackPeriod;
	[SerializeField] protected float projectileSpeed;
	[SerializeField] protected int projectileDamage;

	ProjectileType projectileType;

	// There might be a discard function too
	public void equip(Transform parent) {
		transform.parent = parent;
		transform.localPosition = Vector2.zero;
		new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>()).ForEach((s) => s.enabled = false);
		GetComponent<CircleCollider2D>().enabled = false;
	}

	void IPoolable.reset() { }
	void IPoolable.returnToPool() { }

	// Getters
	public float getAttackRadius() { return attackRadius; }
	public float getAttackPeriod() { return attackPeriod; }
	public float getProjectileSpeed() { return projectileSpeed; }
	public int getProjectileDamage() { return projectileDamage; }
	public abstract ProjectileType getProjectileType();
}
