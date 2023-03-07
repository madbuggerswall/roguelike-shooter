using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Collectible {
	[SerializeField] int healthBuff;

	public override void onCollect() {
		base.onCollect();
		LevelManager.getInstance().getParticles().spawnParticles(this);
		LevelManager.getInstance().getSoundManager().getCollectibleSound().play(this);
	}
}
