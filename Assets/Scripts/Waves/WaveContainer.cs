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

			Prefabs prefabs = LevelManager.getInstance().getPrefabs();
			Enemy jelly = prefabs.getEnemy<Jelly>();
			Enemy ghost = prefabs.getEnemy<Ghost>();
			Enemy brute = prefabs.getEnemy<Brute>();
			Enemy wizard = prefabs.getEnemy<Wizard>();

			// queue.Enqueue(new Wave(jelly, jelly, jelly, jelly));
			queue.Enqueue(new Wave((4, jelly), (2, wizard)));
			queue.Enqueue(new Wave((6, jelly)));
			queue.Enqueue(new Wave((2, ghost)));
			queue.Enqueue(new Wave((4, jelly), (2, ghost)));
			queue.Enqueue(new Wave((8, jelly), (2, ghost)));
			queue.Enqueue(new Wave((4, jelly), (4, ghost)));
			queue.Enqueue(new Wave((2, brute)));
			queue.Enqueue(new Wave((4, jelly), (2, brute)));
			queue.Enqueue(new Wave((8, jelly), (2, brute)));
			queue.Enqueue(new Wave((4, jelly), (4, brute), (4, ghost)));
			queue.Enqueue(new Wave((2, wizard)));

			Debug.Log("Total wave count: " + queue.Count);
		}

		public Queue<Wave> getQueue() { return queue; }
	}
}
