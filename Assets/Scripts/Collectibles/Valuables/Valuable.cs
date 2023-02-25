using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Valuable : MonoBehaviour, ICollectible {
	[SerializeField] int coinValue;

	void ICollectible.onCollect() {
		gameObject.SetActive(false);
		LevelManager.getInstance().getParticles().spawnParticles(transform.position);
	}

	public int getCoinValue() { return coinValue; }
}
