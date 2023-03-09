using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Collectible {
	[SerializeField] int healthBuff;

	public override void onCollect() {
		LevelManager.getInstance().getParticles().spawnParticles(this);
		LevelManager.getInstance().getSoundManager().getCollectibleSound().play(this);
		LevelManager.getInstance().getHero().addHealth(healthBuff);
		returnToPool();
	}

	// IPoolable
	public override void reset() {
		throw new System.NotImplementedException();
	}

	public override void returnToPool() {
		gameObject.SetActive(false);
	}

	public int getHealthBuff() { return healthBuff; }
}
