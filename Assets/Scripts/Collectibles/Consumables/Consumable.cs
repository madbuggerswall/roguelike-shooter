using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour, ICollectible {
	[SerializeField] int healthBuff;

	// ICollectible
	public void onCollect() {
		gameObject.SetActive(false);
	}
}
