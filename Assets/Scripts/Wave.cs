using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be a scriptable object.
public class Wave {
	float period;
	Queue<Enemy> enemyQueue;

	// Gist is it will be a queue of prefab references rather than a enum queue.
	public Wave(float period) {
		this.period = period;
		enemyQueue = new Queue<Enemy>();
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
