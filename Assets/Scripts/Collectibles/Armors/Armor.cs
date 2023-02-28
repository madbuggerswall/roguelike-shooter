using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Armor : Collectible {
	[SerializeField] int shieldAmount;

	public override void reset() { throw new System.NotImplementedException(); }
	public override void returnToPool() { throw new System.NotImplementedException(); }
	public override void onCollect() { gameObject.SetActive(false); }

	public int getShieldAmount() { return shieldAmount; }
}
