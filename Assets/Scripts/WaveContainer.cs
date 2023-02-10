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
		Enemy enemy = Prefabs.getInstance().getEnemy();

		Wave wave1 = new Wave(4f);
		wave1.setEnemies(
			enemy,
			enemy,
			enemy,
			enemy);

		Wave wave2 = new Wave(4f);
		wave2.setEnemies(
			(8, enemy),
			(4, enemy));


		queue = new Queue<Wave>();
		queue.Enqueue(wave1);
		queue.Enqueue(wave2);
	}

	public Queue<Wave> getQueue() { return queue; }
}
