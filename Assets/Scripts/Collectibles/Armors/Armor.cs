using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Armor : Collectible {
	[SerializeField] int shieldAmount;

	public override void onCollect() {
		LevelManager.getInstance().getParticles().spawnParticles(this);
	}

	public int getShieldAmount() { return shieldAmount; }
}
