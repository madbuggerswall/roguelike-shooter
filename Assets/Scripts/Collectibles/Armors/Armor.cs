using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Armor : MonoBehaviour, ICollectible {
	[SerializeField] int shieldAmount;

	// ICollectible
	public void onCollect() {
		gameObject.SetActive(false);
	}
	
	public int getShieldAmount() { return shieldAmount; }
}
