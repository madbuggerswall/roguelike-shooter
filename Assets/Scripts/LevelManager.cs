using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mediator
public class LevelManager : MonoBehaviour {
	static LevelManager instance;

	Player player;

	void Awake() {
		assertSingleton();

		// GameManager
		Application.targetFrameRate = 60;

		// Initialize Layers for easy access
		Layers.initialize();
		
		// Find mediated objects
		player = FindObjectOfType<Player>();

	}

	public Player getPlayer() { return player; }

	// Singleton
	public static LevelManager getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
