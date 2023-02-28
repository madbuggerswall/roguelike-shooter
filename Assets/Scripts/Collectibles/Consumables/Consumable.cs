using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour, IPoolable, ICollectible {
	[SerializeField] int healthBuff;

	public void reset() { throw new System.NotImplementedException(); }
	public void returnToPool() { throw new System.NotImplementedException(); }

	// ICollectible
	public void onCollect() {
		gameObject.SetActive(false);
	}
}
