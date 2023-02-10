using UnityEngine.Events;

class Events {
	static Events instance;

	public UnityEvent playerHit;
	public UnityEvent playerNoticed;

	public UnityEvent projectileThrown;

	public UnityEvent enemyHit;

	Events() {
		playerHit = new UnityEvent();
		playerNoticed = new UnityEvent();

		projectileThrown = new UnityEvent();

		enemyHit = new UnityEvent();
	}

	// Singleton
	public static Events getInstance() {
		if (instance == null)
			instance = new Events();
		return instance;
	}
}