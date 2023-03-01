using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : MonoBehaviour, IPoolable {
	public abstract void reset();
	public abstract void returnToPool();
	public abstract void onCollect();
}