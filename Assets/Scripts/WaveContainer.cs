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
		Wave wave1 = new Wave(1f);
		wave1.setEnemies(
			EnemyType.jelly,
			EnemyType.jelly,
			EnemyType.jelly,
			EnemyType.jelly);

		Wave wave2 = new Wave(1f);
		wave2.setEnemies(
			(8, EnemyType.jelly),
			(4, EnemyType.ghost));

		Wave wave3 = new Wave(0.2f);
		wave3.setEnemies(
			(12, EnemyType.jelly),
			(12, EnemyType.ghost));


		queue = new Queue<Wave>();
		queue.Enqueue(wave1);
		queue.Enqueue(wave2);
		queue.Enqueue(wave3);
	}

	public Queue<Wave> getQueue() { return queue; }
}
