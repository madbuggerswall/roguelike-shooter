using UnityEngine;
using UnityEngine.Events;

class Events {
	static Events instance;

	public UnityEvent<Collision2D> playerHit;
	public UnityEvent playerNoticed;

	public UnityEvent projectileThrown;

	public UnityEvent<Collision2D> enemyHit;
	public UnityEvent<EnemyType> enemyBeaten;

	public UnityEvent<int> waveBegan;

	Events() {
		playerHit = new UnityEvent<Collision2D>();
		playerNoticed = new UnityEvent();

		projectileThrown = new UnityEvent();

		enemyHit = new UnityEvent<Collision2D>();
		enemyBeaten = new UnityEvent<EnemyType>();

		waveBegan = new UnityEvent<int>();
	}

	// Singleton
	public static Events getInstance() {
		if (instance == null)
			instance = new Events();
		return instance;
	}
}