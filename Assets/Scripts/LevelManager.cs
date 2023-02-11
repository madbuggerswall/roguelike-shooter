using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Make the projectiles rigidbodies and move linear.

// Mediator
public class LevelManager : MonoBehaviour {
	static LevelManager instance;

	Player player;

	void Awake() {
		assertSingleton();

		// Find mediated objects
		player = FindObjectOfType<Player>();
	}

	public Player getPlayer() { return player; }

	// Singleton
	public static LevelManager getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
