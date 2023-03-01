using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShooter : MonoBehaviour {
	Transform target;
	bool isEngaging = false;

	void Start() {
		StartCoroutine(checkEnemiesAround(6, 0.2f));
	}

	IEnumerator checkEnemiesAround(float radius, float period) {
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(Layers.enemyMask);
		List<Collider2D> enemiesAround = new List<Collider2D>();

		while (true) {
			Physics2D.OverlapCircle(transform.position, radius, contactFilter, enemiesAround);
			target = getClosestEnemy(enemiesAround);

			if (!isEngaging && target is not null)
				StartCoroutine(attackPeriodically());

			yield return new WaitForSeconds(period);
		}
	}

	Transform getClosestEnemy(List<Collider2D> enemiesAround) {
		float closestDistanceSqr = Mathf.Infinity;
		Transform closestEnemy = null;

		foreach (Collider2D enemy in enemiesAround) {
			float distanceSqr = (transform.position - enemy.transform.position).sqrMagnitude;
			if (distanceSqr < closestDistanceSqr) {
				closestDistanceSqr = distanceSqr;
				closestEnemy = enemy.transform;
			}
		}
		return closestEnemy;
	}

	IEnumerator attackPeriodically() {
		isEngaging = true;
		Inventory inventory = GetComponentInChildren<Inventory>();
		Weapon weapon = inventory.getWeapon();

		// Attack while target is active, and hero is not being dragged
		while (target is not null && target.gameObject.activeInHierarchy && weapon is not null) {
			throwProjectile(target, weapon);
			yield return new WaitForSeconds(weapon.getAttackPeriod());
		}

		isEngaging = false;
	}

	// Spawn and throw a projectile, setting its target and damage value
	void throwProjectile(Transform target, Weapon weapon) {
		ObjectPool objectPool = ProjectileContainer.getInstance().GetComponentInChildren<ObjectPool>();
		Projectile projectile = objectPool.spawn(weapon.getProjectilePrefab(), transform.position);
		projectile.throwAtTarget(target, weapon.getProjectileDamage(), weapon.getProjectileSpeed());
		Events.getInstance().projectileThrown.Invoke();
	}
}
