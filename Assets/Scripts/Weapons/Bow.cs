using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Weapon {
	public override Projectile getProjectilePrefab() {
		return Prefabs.getInstance().getProjectile<Arrow>();
	}
}