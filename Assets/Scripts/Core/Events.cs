using UnityEngine;
using UnityEngine.Events;

class Events {
	static Events instance;

	public UnityEvent<Enemy> enemyBeaten;
	public UnityEvent heroBeaten;
	public UnityEvent<int> waveBegan;

	Events() {
		enemyBeaten = new UnityEvent<Enemy>();
		heroBeaten = new UnityEvent();
		waveBegan = new UnityEvent<int>();
	}

	// Singleton
	public static Events getInstance() {
		if (instance == null)
			instance = new Events();
		return instance;
	}
}