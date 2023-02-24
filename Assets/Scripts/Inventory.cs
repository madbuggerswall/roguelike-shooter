using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// IEquipable, ICollectible, Collectible, Upgrade

public class Inventory : MonoBehaviour {
	Weapon weapon;

	void Start() {

	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.layer == Layers.weapon) {
			Weapon weapon = other.gameObject.GetComponent<Weapon>();
			this.weapon = weapon;
			weapon.equip(transform);
		}
	}
	
	IEnumerator checkForItems(float radius, float period) {
		ContactFilter2D contactFilter = new ContactFilter2D();
		contactFilter.SetLayerMask(Layers.enemyMask);
		Collider2D[] enemiesAround = new Collider2D[8];

		while (true) {
			yield return new WaitForSeconds(period);

			int enemyCount = Physics2D.OverlapCircle(transform.position, radius, contactFilter, enemiesAround);
			// if (enemyCount == 0) {
			// 	target = null;
			// 	continue;
			// }

			// target = getClosestEnemy(enemiesAround, enemyCount);

			// if (!isEngaging && weapon is not null)
			// 	StartCoroutine(attackPeriodically(weapon.getAttackPeriod()));
		}
	}
}
