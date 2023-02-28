using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rename this to buff
public abstract class Upgrade : MonoBehaviour, ICollectible {
	public abstract void apply();

	// ICollectible
	public void onCollect() {
		gameObject.SetActive(false);
	}
}
