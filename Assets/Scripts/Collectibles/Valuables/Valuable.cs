using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Valuable : Collectible {
	[SerializeField] int coinValue;

	public override void onCollect() {
		base.onCollect();
		LevelManager.getInstance().getParticles().spawnParticles(this);
		LevelManager.getInstance().getSoundManager().getCollectibleSound().play(this);
	}

	public int getCoinValue() { return coinValue; }
}
