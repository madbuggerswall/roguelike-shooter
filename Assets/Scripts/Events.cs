using UnityEngine.Events;

class Events {
	static Events instance;

	public UnityEvent playerHit;


	Events() {
		playerHit = new UnityEvent();
	}

	// Singleton
	public static Events getInstance() {
		if (instance == null)
			instance = new Events();
		return instance;
	}
}