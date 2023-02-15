using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IPoolable {
	[SerializeField] protected float attackRadius;
	[SerializeField] protected float attackPeriod;
	[SerializeField] protected float projectileSpeed;
	[SerializeField] protected int projectileDamage;

	ProjectileType projectileType;

	public abstract void attack(Transform target);
	// public abstract void attack();

	// There might be a discard function too
	public void equip(Transform parent) {
		transform.parent = parent;
		GetComponent<SpriteRenderer>().enabled = false;
		new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>()).ForEach((s) => s.enabled = false);
		GetComponent<CircleCollider2D>().enabled = false;
	}

	void IPoolable.reset() { }
	void IPoolable.returnToPool() { }

	// Spawn and throw a projectile, setting its target and damage value
	protected void throwProjectile(Transform target) {
		ObjectPool objectPool = ProjectileContainer.getInstance().GetComponentInChildren<ObjectPool>();
		Projectile projectilePrefab = Prefabs.getInstance().getProjectile(getProjectileType());
		Projectile projectile = objectPool.spawn(projectilePrefab.gameObject, transform.position).GetComponent<Projectile>();
		projectile.throwAtTarget(target, projectileDamage, projectileSpeed);
		Events.getInstance().projectileThrown.Invoke();
	}

	// Getters
	public float getAttackRadius() { return attackRadius; }
	public float getAttackPeriod() { return attackPeriod; }
	public float getProjectileSpeed() { return projectileSpeed; }
	public int getProjectileDamage() { return projectileDamage; }
	public abstract ProjectileType getProjectileType();
}
