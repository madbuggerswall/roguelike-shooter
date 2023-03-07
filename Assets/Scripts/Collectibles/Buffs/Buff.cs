using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rename this to buff
public abstract class Buff : Collectible {
	public abstract void apply();

	public override void onCollect() {
		LevelManager.getInstance().getParticles().spawnParticles(this);
		LevelManager.getInstance().getSoundManager().getCollectibleSound().play(this);
	}
}
