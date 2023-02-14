using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO Spawn points
public class EnemySpawner : MonoBehaviour {
	const float waveBreak = 2f;

	WaveContainer waveContainer;
	ObjectPool objectPool;

	int waveCount;
	int enemiesSpawned;

	void Awake() {
		waveContainer = new WaveContainer();
		objectPool = GetComponentInChildren<ObjectPool>();
	}

	void Start() {
		// Events.getInstance().gameOver.AddListener(delegate { StopAllCoroutines(); });

		spawnNextWave();
		Events.getInstance().enemyBeaten.AddListener(delegate { checkForNextWave(); });
	}

	void checkForNextWave() {
		enemiesSpawned--;
		if (enemiesSpawned == 0)
			spawnNextWave();
	}

	void spawnNextWave() {
		Events.getInstance().waveBegan.Invoke(waveCount++);
		Wave currentWave = waveContainer.getQueue().Dequeue();
		enemiesSpawned = currentWave.getEnemyQueue().Count;
		StartCoroutine(spawnEnemies(currentWave));
	}

	// Spawn waves periodically every waveBreak seconds
	IEnumerator spawnWaves(WaveContainer waveContainer) {
		Queue<Wave> waveQueue = waveContainer.getQueue();

		for (int waveCount = 1; waveQueue.Count > 0; waveCount++) {
			Events.getInstance().waveBegan.Invoke(waveCount);
			yield return new WaitForSeconds(waveBreak);
			yield return spawnEnemies(waveQueue.Dequeue());
		}
	}

	// Spawn enemies periodically every Wave.period seconds
	IEnumerator spawnEnemies(Wave wave) {
		Queue<EnemyType> enemyQueue = wave.getEnemyQueue();

		while (enemyQueue.Count > 0) {
			Enemy enemyPrefab = Prefabs.getInstance().getEnemy(enemyQueue.Dequeue());
			objectPool.spawn(enemyPrefab.gameObject, transform.position);
			yield return new WaitForSeconds(wave.getPeriod());
		}
	}
}
