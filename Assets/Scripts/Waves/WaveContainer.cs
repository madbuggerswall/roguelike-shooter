using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Waves {
	public class WaveContainer {
		Queue<Wave> queue;

		public WaveContainer() {
			initializeWaves();
		}

		// Manual creation of waves. Monsters spawn more frequently as waves proceed
		void initializeWaves() {
			queue = new Queue<Wave>();

			queue.Enqueue(new Wave(
				EnemyType.jelly,
				EnemyType.jelly,
				EnemyType.jelly,
				EnemyType.jelly
			));

			queue.Enqueue(new Wave(
				(6, EnemyType.jelly)
			));

			queue.Enqueue(new Wave(
				(6, EnemyType.jelly),
				(2, EnemyType.ghost)
			));

			queue.Enqueue(new Wave(
				(8, EnemyType.jelly),
				(2, EnemyType.ghost)
			));

			queue.Enqueue(new Wave(
				(4, EnemyType.jelly),
				(4, EnemyType.ghost)
			));

			queue.Enqueue(new Wave(
				(12, EnemyType.jelly),
				(12, EnemyType.ghost)
			));

			queue.Enqueue(new Wave(
				(4, EnemyType.jelly),
				(4, EnemyType.brute),
				(4, EnemyType.ghost)
			));

			Debug.Log("Total wave count: " + queue.Count);
		}

		public Queue<Wave> getQueue() { return queue; }
	}
}
