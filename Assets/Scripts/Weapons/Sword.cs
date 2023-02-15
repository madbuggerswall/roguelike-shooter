using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
	
	// Attack function could hold sound and particle FX and other behavior
	public override void attack(Transform target) {
		throwProjectile(target);
	}

	public override ProjectileType getProjectileType() { return ProjectileType.sword; }
}
