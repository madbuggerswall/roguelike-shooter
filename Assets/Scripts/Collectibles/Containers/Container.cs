using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Container : MonoBehaviour, IPoolable, ICollectible {
	public void reset() { throw new System.NotImplementedException(); }
	public void returnToPool() { throw new System.NotImplementedException(); }

	public void onCollect() { throw new System.NotImplementedException(); }
}
