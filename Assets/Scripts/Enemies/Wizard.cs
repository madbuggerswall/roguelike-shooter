using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy {
	Weapon weapon;

	protected override void Awake() {
		base.Awake();
		weapon = LevelManager.getInstance().getPrefabs().getWeapon<Scepter>();
		attackRadius = weapon.getAttackRadius();
	}

	protected override IEnumerator chaseAndAttack() {
		Transform target = LevelManager.getInstance().getHero().transform;

		float noticeDuration = 0.2f;

		// While player is alive/not beaten
		while (true) {
			yield return chase(target);
			yield return notice(noticeDuration);
			yield return attackPeriodically(target);
		}
	}

	IEnumerator attackPeriodically(Transform target) {
		System.Func<Vector2, Vector2, float, bool> inRadius = (a, b, r) => (a - b).SqrMagnitude() <= r * r;
		while (inRadius(target.position, rigidBody.position, attackRadius)) {
			throwProjectile(target, weapon);
			yield return new WaitForSeconds(weapon.getAttackPeriod());
		}
	}

	// Spawn and throw a projectile, setting its target and damage value
	void throwProjectile(Transform target, Weapon weapon) {
		ObjectPool objectPool = ProjectileContainer.getInstance().GetComponentInChildren<ObjectPool>();
		Projectile projectile = objectPool.spawn(weapon.getProjectilePrefab(), transform.position);
		projectile.gameObject.layer = Layers.enemyProjectile;
		projectile.throwAtTarget(target, weapon.getProjectileDamage(), weapon.getProjectileSpeed());
		Events.getInstance().projectileThrown.Invoke();
	}
}
