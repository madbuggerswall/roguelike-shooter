using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Container : Collectible {
	public override void onCollect() {
		LevelManager.getInstance().getParticles().spawnParticles(this);
	}
	// IPoolable
	public override void reset() {
		throw new System.NotImplementedException();
	}

	public override void returnToPool() {
		gameObject.SetActive(false);
	}
}
