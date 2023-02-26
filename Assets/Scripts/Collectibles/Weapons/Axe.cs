using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : Weapon {
	public override Projectile getProjectilePrefab() {
		return LevelManager.getInstance().getPrefabs().getProjectile<AxeProjectile>();
	}
}
