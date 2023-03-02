using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Container : Collectible {
	public override void onCollect() {
		base.onCollect();
		LevelManager.getInstance().getParticles().spawnParticles(this);
	}
}
