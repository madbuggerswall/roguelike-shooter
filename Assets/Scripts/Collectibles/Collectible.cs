using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour, IPoolable {
	public abstract void onCollect();

	public abstract void reset();
	public abstract void returnToPool();
}