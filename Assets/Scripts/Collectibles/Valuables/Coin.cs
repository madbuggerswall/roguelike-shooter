using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Valuable {
	public override void onCollect() {
		base.onCollect();
		LevelManager.getInstance().getParticles().spawnParticles(transform.position);
		LevelManager.getInstance().getSoundManager().playCoinCollected();
	}
}
