using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Valuable : MonoBehaviour, ICollectible {
	[SerializeField] int coinValue;

	public virtual void onCollect() {
		gameObject.SetActive(false);
	}

	public int getCoinValue() { return coinValue; }
}
