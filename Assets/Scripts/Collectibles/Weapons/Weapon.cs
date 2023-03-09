using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO Struct WeaponProperties

public abstract class Weapon : Collectible {
	[SerializeField] protected float baseAttackRadius;
	[SerializeField] protected float baseAttackPeriod;
	[SerializeField] protected float baseProjectileSpeed;
	[SerializeField] protected int baseProjectileDamage;

	[SerializeField] protected float attackRadius;
	[SerializeField] protected float attackPeriod;
	[SerializeField] protected float projectileSpeed;
	[SerializeField] protected int projectileDamage;

	public override void onCollect() {
		LevelManager.getInstance().getParticles().spawnParticles(this);
		LevelManager.getInstance().getSoundManager().getCollectibleSound().play(this);
	}

	// IPoolable
	public override void reset() {
		attackRadius = baseAttackRadius;
		attackPeriod = baseAttackPeriod;
		projectileSpeed = baseProjectileSpeed;
		projectileDamage = baseProjectileDamage;
	}

	public override void returnToPool() {
		gameObject.SetActive(false);
		transform.SetParent(LevelManager.getInstance().getDropManager().getPoolTransform());
	}

	// Setters
	public void setAttackRadius(float attackRadius) { this.attackRadius = attackRadius; }
	public void setAttackPeriod(float attackPeriod) { this.attackPeriod = attackPeriod; }
	public void setProjectileSpeed(float projectileSpeed) { this.projectileSpeed = projectileSpeed; }
	public void setProjectileDamage(int projectileDamage) { this.projectileDamage = projectileDamage; }

	// Getters
	public float getAttackRadius() { return attackRadius; }
	public float getAttackPeriod() { return attackPeriod; }
	public float getProjectileSpeed() { return projectileSpeed; }
	public int getProjectileDamage() { return projectileDamage; }
	
	public float getBaseAttackRadius() { return baseAttackRadius; }
	public float getBaseAttackPeriod() { return baseAttackPeriod; }
	public float getBaseProjectileSpeed() { return baseProjectileSpeed; }
	public int getBaseProjectileDamage() { return baseProjectileDamage; }
	
	public abstract Projectile getProjectilePrefab();
}
