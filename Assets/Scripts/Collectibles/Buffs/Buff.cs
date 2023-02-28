using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rename this to buff
public abstract class Buff : MonoBehaviour, IPoolable, ICollectible {
	public abstract void apply();

	public void reset() { throw new System.NotImplementedException(); }
	public void returnToPool() { throw new System.NotImplementedException(); }

	// ICollectible
	public void onCollect() {
		gameObject.SetActive(false);
	}
}
