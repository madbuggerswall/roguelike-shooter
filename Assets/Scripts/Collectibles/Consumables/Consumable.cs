using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : Collectible {
	[SerializeField] int healthBuff;

	public override void reset() { throw new System.NotImplementedException(); }
	public override void returnToPool() { throw new System.NotImplementedException(); }
	public override void onCollect() { gameObject.SetActive(false); }
}
