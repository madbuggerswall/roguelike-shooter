using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	const float waveBreak = 2f;

	WaveContainer waveContainer;
	ObjectPool objectPool;

	void Awake() {
		waveContainer = new WaveContainer();
		objectPool = GetComponentInChildren<ObjectPool>();
	}

	void Start() {
		// Events.getInstance().gameOver.AddListener(delegate { StopAllCoroutines(); });
		StartCoroutine(spawnWaves(waveContainer));
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
