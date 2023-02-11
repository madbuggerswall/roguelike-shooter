using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveContainer {
	Queue<Wave> queue;

	public WaveContainer() {
		initializeWaves();
	}

	// Manual creation of waves. Monsters spawn more frequently as waves proceed
	void initializeWaves() {
		Wave wave1 = new Wave(4f);
		wave1.setEnemies(
			EnemyType.jelly,
			EnemyType.jelly,
			EnemyType.jelly,
			EnemyType.jelly);

		Wave wave2 = new Wave(4f);
		wave2.setEnemies(
			(8, EnemyType.jelly),
			(4, EnemyType.ghost));


		queue = new Queue<Wave>();
		queue.Enqueue(wave1);
		queue.Enqueue(wave2);
	}

	public Queue<Wave> getQueue() { return queue; }
}
