using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IPoolable, IEquipable {
	[SerializeField] protected float attackRadius;
	[SerializeField] protected float attackPeriod;
	[SerializeField] protected float projectileSpeed;
	[SerializeField] protected int projectileDamage;

	ProjectileType projectileType;

	// There might be a discard function too
	void IEquipable.onEquip() {
		transform.localPosition = Vector2.zero;
		new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>()).ForEach((s) => s.enabled = false);
		GetComponent<CircleCollider2D>().enabled = false;
		GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
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
