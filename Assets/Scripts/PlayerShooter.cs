using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
	bool isEngaging = false;
	Transform target;

	void Start() {
		StartCoroutine(checkEnemiesAround(6, 0.2f));
	}

	IEnumerator checkEnemiesAround(float radius, float period) {
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(Layers.enemyMask);
		List<Collider2D> enemiesAround = new List<Collider2D>();

		Inventory inventory = GetComponentInChildren<Inventory>();

		while (true) {
			yield return new WaitForSeconds(period);

			Physics2D.OverlapCircle(transform.position, radius, contactFilter, enemiesAround);
			target = getClosestEnemy(enemiesAround);

			if (!isEngaging && inventory.getWeapon() is not null)
				StartCoroutine(attackPeriodically(inventory.getWeapon()));
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

	IEnumerator attackPeriodically(Weapon weapon) {
		isEngaging = true;

		// Attack while target is active, and hero is not being dragged
		while (target != null && target.gameObject.activeInHierarchy) {
			throwProjectile(target, weapon);
			yield return new WaitForSeconds(weapon.getAttackPeriod());
		}

		target = null;
		isEngaging = false;
	}

	// Spawn and throw a projectile, setting its target and damage value
	protected void throwProjectile(Transform target, Weapon weapon) {
		ObjectPool objectPool = ProjectileContainer.getInstance().GetComponentInChildren<ObjectPool>();
		Projectile projectilePrefab = Prefabs.getInstance().getProjectile(weapon.getProjectileType());
		Projectile projectile = objectPool.spawn(projectilePrefab.gameObject, transform.position).GetComponent<Projectile>();
		projectile.throwAtTarget(target, weapon.getProjectileDamage(), weapon.getProjectileSpeed());
		Events.getInstance().projectileThrown.Invoke();
	}
}
