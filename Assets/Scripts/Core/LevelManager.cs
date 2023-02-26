using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mediator
public class LevelManager : MonoBehaviour {
	static LevelManager instance;

	Player player;

	Particles particles;
	SoundManager soundManager;

	void Awake() {
		assertSingleton();

		// GameManager
		Application.targetFrameRate = 60;

		// Initialize Layers for easy access
		Layers.initialize();

		// Find mediated objects
		player = FindObjectOfType<Player>();
		particles = FindObjectOfType<Particles>();
		soundManager = FindObjectOfType<SoundManager>();
	}

	public Player getPlayer() { return player; }
	public Particles getParticles() { return particles; }
	public SoundManager getSoundManager() { return soundManager; }

	// Singleton
	public static LevelManager getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
