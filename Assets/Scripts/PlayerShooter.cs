using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
	bool isEngaging = false;
	Transform target;

	[SerializeField] Weapon weapon;

	void Start() {
		StartCoroutine(checkRadiusPeriodically(6, 0.2f));
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == Layers.weapon) {
			Weapon weapon = other.gameObject.GetComponent<Weapon>();
			this.weapon = weapon;
			weapon.equip(transform);
		}
	}

	// Check radius for enemies, sort them by their path progress, make the first one the target
	// TODO Faulty behavior
	IEnumerator checkRadiusPeriodically(float radius, float period) {
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(Layers.enemyMask);
		Collider2D[] enemiesAround = new Collider2D[8];

		while (true) {
			yield return new WaitForSeconds(period);

			int enemyCount = Physics2D.OverlapCircle(transform.position, radius, contactFilter, enemiesAround);
			if (enemyCount == 0) {
				target = null;
				continue;
			}

			target = getClosestEnemy(enemiesAround, enemyCount);

			if (!isEngaging && weapon is not null)
				StartCoroutine(attackPeriodically(weapon.getAttackPeriod()));
		}
	}

	Transform getClosestEnemy(Collider2D[] enemiesAround, int enemyCount) {
		float closestDistanceSqr = Mathf.Infinity;
		Transform closestEnemy = null;
		for (int i = 0; i < enemyCount; i++) {
			float distanceSqr = Vector2.Dot(transform.position, enemiesAround[i].transform.position);
			if (distanceSqr < closestDistanceSqr) {
				closestDistanceSqr = distanceSqr;
				closestEnemy = enemiesAround[i].transform;
			}
		}
		return closestEnemy;
	}

	IEnumerator attackPeriodically(float period) {
		isEngaging = true;

		// Attack while target is active, and hero is not being dragged
		while (target != null && target.gameObject.activeInHierarchy) {
			weapon.attack(target);
			yield return new WaitForSeconds(period);
		}

		target = null;
		isEngaging = false;
	}
}
