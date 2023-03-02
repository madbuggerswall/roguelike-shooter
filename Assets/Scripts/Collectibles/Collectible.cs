using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour, IPoolable {
	public virtual void onCollect() {
		returnToPool();
	}

	public virtual void reset() { }
	public virtual void returnToPool() { gameObject.SetActive(false); }
}