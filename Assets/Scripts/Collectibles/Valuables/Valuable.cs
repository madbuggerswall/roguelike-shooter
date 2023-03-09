using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Valuable : Collectible {
	[SerializeField] int coinValue;

	public override void onCollect() {
		LevelManager.getInstance().getParticles().spawnParticles(this);
		LevelManager.getInstance().getSoundManager().getCollectibleSound().play(this);
		returnToPool();
	}

	// IPoolable
	public override void reset() {
		throw new System.NotImplementedException();
	}

	public override void returnToPool() {
		gameObject.SetActive(false);
	}

	public int getCoinValue() { return coinValue; }
}
