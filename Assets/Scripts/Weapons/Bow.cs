using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {
	public override void attack(Transform target) {
		throwProjectile(target);
	}

	public override ProjectileType getProjectileType() { return ProjectileType.arrow; }
}