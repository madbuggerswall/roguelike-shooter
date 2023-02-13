using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
	[SerializeField] Projectile projectilePrefab;
	[SerializeField] float attackPeriod;
	[SerializeField] int projectileDamage;

	bool isEngaging = false;
	Transform target;

	void Start() {
		StartCoroutine(checkRadiusPeriodically(6, 0.5f));
	}

	// Check radius for enemies, sort them by their path progress, make the first one the target
	IEnumerator checkRadiusPeriodically(float radius, float period) {
		// OverlapCircle boilerplate
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
		Collider2D[] enemiesAround = new Collider2D[8];

		while (true) {
			yield return new WaitForSeconds(period);

			// Get enemy colliders in range
			int enemyCount = Physics2D.OverlapCircle(transform.position, radius, contactFilter, enemiesAround);
			
			if (enemyCount == 0)
				target = null;

			float closestDistanceSqr = Mathf.Infinity;
			for (int i = 0; i < enemyCount; i++) {
				float distanceSqr = Vector2.Dot(transform.position, enemiesAround[i].transform.position);
				if (distanceSqr < closestDistanceSqr) {
					closestDistanceSqr = distanceSqr;
					target = enemiesAround[i].transform;
				}
			}

			// Start firing if it isn't already
			if (!isEngaging)
				StartCoroutine(attackPeriodically(attackPeriod));
		}
	}

	// Attack function could hold sound and particle FX and other behavior
	void attack(Transform target) {
		throwProjectile(target);
	}

	// Spawn and throw a projectile, setting its target and damage value
	void throwProjectile(Transform target) {
		ObjectPool objectPool = ProjectileContainer.getInstance().GetComponentInChildren<ObjectPool>();
		Projectile projectile = objectPool.spawn(projectilePrefab.gameObject, transform.position).GetComponent<Projectile>();
		projectile.throwAtTarget(target, projectileDamage);
		Events.getInstance().projectileThrown.Invoke();
	}

	IEnumerator attackPeriodically(float period) {
		isEngaging = true;

		// Attack while target is active, and hero is not being dragged
		while (target != null && target.gameObject.activeInHierarchy) {
			attack(target);
			yield return new WaitForSeconds(period);
		}

		target = null;
		isEngaging = false;
	}
}
