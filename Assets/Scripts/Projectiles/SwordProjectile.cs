using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordProjectile : Projectile {
	protected override ProjectileType getProjectileType() { return ProjectileType.sword; }
}
