using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mediator
[DefaultExecutionOrder(-4)]
public class LevelManager : MonoBehaviour {
	static LevelManager instance;

	Hero hero;
	Prefabs prefabs;
	Particles particles;
	DropManager dropManager;
	SoundManager soundManager;
	CameraImpulse cameraImpulse;
	UIManager userInterfaceManager;

	void Awake() {
		assertSingleton();

		// GameManager
		Application.targetFrameRate = 60;

		// Initialize Layers for easy access
		Layers.initialize();

		// Find mediated objects
		hero = FindObjectOfType<Hero>();
		prefabs = FindObjectOfType<Prefabs>();
		particles = FindObjectOfType<Particles>();
		dropManager = FindObjectOfType<DropManager>();
		soundManager = FindObjectOfType<SoundManager>();
		cameraImpulse = FindObjectOfType<CameraImpulse>();
		userInterfaceManager = FindObjectOfType<UIManager>();
	}

	public Hero getHero() { return hero; }
	public Prefabs getPrefabs() { return prefabs; }
	public Particles getParticles() { return particles; }
	public DropManager getDropManager() { return dropManager; }
	public SoundManager getSoundManager() { return soundManager; }
	public CameraImpulse getCameraImpulse() { return cameraImpulse; }
	public UIManager getUIManager() { return userInterfaceManager; }

	// Singleton
	public static LevelManager getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
