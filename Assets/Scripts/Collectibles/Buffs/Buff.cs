using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rename this to buff
public abstract class Buff : Collectible {
	public abstract void apply();

	public override void reset() { throw new System.NotImplementedException(); }
	public override void returnToPool() { throw new System.NotImplementedException(); }
	public override void onCollect() { gameObject.SetActive(false); }
}
