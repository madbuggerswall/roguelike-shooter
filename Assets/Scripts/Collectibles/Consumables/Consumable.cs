using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour, ICollectible {
	[SerializeField] int healthBuff;

	void ICollectible.onCollect() {
		gameObject.SetActive(false);
	}
}
