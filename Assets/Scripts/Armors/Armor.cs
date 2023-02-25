using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Armor : MonoBehaviour, ICollectible {
	[SerializeField] int shieldAmount;

	void ICollectible.onCollect() {
		gameObject.SetActive(false);
	}
	
	public int getShieldAmount() { return shieldAmount; }
}
