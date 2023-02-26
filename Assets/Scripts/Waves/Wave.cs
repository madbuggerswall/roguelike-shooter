using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waves {
	// This should be a scriptable object.
	public class Wave {
		float period;
		Queue<EnemyType> enemyQueue;

		public Wave() {
			this.period = 1f;
			this.enemyQueue = new Queue<EnemyType>();
		}

		public Wave(float period) {
			this.period = period;
			this.enemyQueue = new Queue<EnemyType>();
		}

		public Wave(params EnemyType[] enemyQueue) : this() {
			setEnemies(enemyQueue);
		}

		// Gist is it will be a queue of prefab references rather than a enum queue.
		public Wave(params (int, EnemyType)[] enemyEntries) : this() {
			setEnemies(enemyEntries);
		}

		// Set enemies manually
		public void setEnemies(params EnemyType[] enemyQueue) {
			foreach (EnemyType enemy in enemyQueue) {
				this.enemyQueue.Enqueue(enemy);
			}
		}

		// Set enemies by specifying how much from each enemy type
		public void setEnemies(params (int, EnemyType)[] enemyEntries) {
			foreach ((int count, EnemyType type) enemyEntry in enemyEntries) {
				for (int i = 0; i < enemyEntry.count; i++) {
					enemyQueue.Enqueue(enemyEntry.type);
				}
			}
		}

		// Getters
		public float getPeriod() { return period; }
		public Queue<EnemyType> getEnemyQueue() { return enemyQueue; }
	}
}