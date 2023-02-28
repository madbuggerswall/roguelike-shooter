using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Valuable : MonoBehaviour, IPoolable, ICollectible {
	[SerializeField] int coinValue;

	public void reset() { throw new System.NotImplementedException(); }
	public void returnToPool() { throw new System.NotImplementedException(); }

	public void onCollect() {
		gameObject.SetActive(false);
	}


	public int getCoinValue() { return coinValue; }
}
