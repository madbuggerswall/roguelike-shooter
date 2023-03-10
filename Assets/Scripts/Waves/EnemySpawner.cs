using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Waves {
	public class EnemySpawner : MonoBehaviour {
		const float waveBreak = 2f;

		WaveContainer waveContainer;
		SpawnPoints spawnPoints;
		ObjectPool objectPool;

		int waveCount;
		int enemiesSpawned;

		void Awake() {
			waveContainer = new WaveContainer();
			objectPool = GetComponentInChildren<ObjectPool>();
			spawnPoints = GetComponentInChildren<SpawnPoints>();
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
			Queue<Enemy> enemyQueue = wave.getEnemyQueue();
			Vector2[] spawnPositions = spawnPoints.getPositions();

			while (enemyQueue.Count > 0) {
				Vector2 spawnPosition = spawnPositions[enemyQueue.Count % spawnPositions.Length];
				objectPool.spawn(enemyQueue.Dequeue(), spawnPosition);
				yield return new WaitForSeconds(wave.getPeriod());
			}
		}
	}
}