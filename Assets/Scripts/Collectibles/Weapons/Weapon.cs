using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : Collectible {
	// [SerializeField] protected float baseAttackRadius;
	// [SerializeField] protected float baseAttackPeriod;
	// [SerializeField] protected float baseProjectileSpeed;
	// [SerializeField] protected int baseProjectileDamage;

	[SerializeField] protected float attackRadius;
	[SerializeField] protected float attackPeriod;
	[SerializeField] protected float projectileSpeed;
	[SerializeField] protected int projectileDamage;

	// AudioClip onEquip
	// ParticleSystem onEquip

	public override void reset() { throw new System.NotImplementedException(); }
	public override void returnToPool() { throw new System.NotImplementedException(); }
	public override void onCollect() {
		// gameObject.SetActive(false);
	}

	// Getters
	public float getAttackRadius() { return attackRadius; }
	public float getAttackPeriod() { return attackPeriod; }
	public float getProjectileSpeed() { return projectileSpeed; }
	public int getProjectileDamage() { return projectileDamage; }
	public abstract Projectile getProjectilePrefab();
}
