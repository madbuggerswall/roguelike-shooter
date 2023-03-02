using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Valuable : Collectible {
	[SerializeField] int coinValue;

	public override void onCollect() {
		base.onCollect();
		LevelManager.getInstance().getParticles().spawnParticles(this);
	}

	public int getCoinValue() { return coinValue; }
}
