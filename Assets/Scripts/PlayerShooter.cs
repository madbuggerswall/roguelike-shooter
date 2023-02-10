using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
	[SerializeField] Projectile projectilePrefab;
	[SerializeField] float attackPeriod;
	[SerializeField] int projectileDamage;

	bool isEngaging = false;
	Enemy target;

	void Start() {
		StartCoroutine(checkRadiusPeriodically(6, 2));
	}

	// Check radius for enemies, sort them by their path progress, make the first one the target
	IEnumerator checkRadiusPeriodically(float radius, float period) {
		int layerMask = LayerMask.GetMask("Enemy");
		List<Enemy> enemiesInRange = new List<Enemy>(4);

		while (true) {
			yield return new WaitForSeconds(period);

			// Get enemy colliders in range
			Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

			// Check again if empty
			if (colliders.Length == 0)
				continue;

			// Add detected enemies to a list to be sorted
			for (int i = 0; i < colliders.Length; i++)
				enemiesInRange.Add(colliders[i].GetComponent<Enemy>());

			// Sort detected enemies by distance, make the first one the target
			enemiesInRange.Sort((first, second) => Vector3.Distance(second.transform.position, transform.position).CompareTo(Vector3.Distance(second.transform.position, transform.position)));
			target = enemiesInRange[0];
			enemiesInRange.Clear();

			// Start firing if it isn't already
			if (!isEngaging)
				StartCoroutine(attackPeriodically(attackPeriod));
		}
	}

	// Attack function could hold sound and particle FX and other behavior
	void attack(Enemy enemy) {
		throwProjectile(enemy);
	}

	// Spawn and throw a projectile, setting its target and damage value
	void throwProjectile(Enemy target) {
		ObjectPool objectPool = ProjectileContainer.getInstance().GetComponentInChildren<ObjectPool>();
		Projectile projectile = objectPool.spawn(projectilePrefab.gameObject, transform.position).GetComponent<Projectile>();
		projectile.throwAtTarget(target, projectileDamage);
		Events.getInstance().projectileThrown.Invoke();
	}

	IEnumerator attackPeriodically(float period) {
		isEngaging = true;

		// Attack while target is active, and hero is not being dragged
		while (target != null && target.gameObject.activeInHierarchy) {
			yield return new WaitForSeconds(period);
			attack(target);
		}

		target = null;
		isEngaging = false;
	}
}
