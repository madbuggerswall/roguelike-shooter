using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is for accessing the global object pool for projectiles
public class ProjectileContainer : MonoBehaviour {
	static ProjectileContainer instance;

	ObjectPool objectPool;

	void Awake() {
		assertSingleton();
		objectPool = GetComponentInChildren<ObjectPool>();
	}

	// Singleton
	public static ProjectileContainer getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
