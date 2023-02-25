using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour, ICollectible {
	public abstract void apply();

	void ICollectible.onCollect() {
		gameObject.SetActive(false);
	}
}
