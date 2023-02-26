using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waves {
	// This should be a scriptable object.
	public class Wave {
		float period;
		Queue<Enemy> enemyQueue;

		public Wave() {
			this.period = 1f;
			this.enemyQueue = new Queue<Enemy>();
		}

		public Wave(float period) {
			this.period = period;
			this.enemyQueue = new Queue<Enemy>();
		}

		public Wave(params Enemy[] enemyQueue) : this() {
			setEnemies(enemyQueue);
		}

		// Gist is it will be a queue of prefab references rather than a enum queue.
		public Wave(params (int, Enemy)[] enemyEntries) : this() {
			setEnemies(enemyEntries);
		}

		// Set enemies manually
		public void setEnemies(params Enemy[] enemyQueue) {
			foreach (Enemy enemy in enemyQueue) {
				this.enemyQueue.Enqueue(enemy);
			}
		}

		// Set enemies by specifying how much from each enemy type
		public void setEnemies(params (int, Enemy)[] enemyEntries) {
			foreach ((int count, Enemy type) enemyEntry in enemyEntries) {
				for (int i = 0; i < enemyEntry.count; i++) {
					enemyQueue.Enqueue(enemyEntry.type);
				}
			}
		}

		// Getters
		public float getPeriod() { return period; }
		public Queue<Enemy> getEnemyQueue() { return enemyQueue; }
	}
}