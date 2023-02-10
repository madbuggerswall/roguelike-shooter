using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
	const float waveBreak = 4f;

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
		int waveCount = 1;

		Queue<Wave> waveQueue = waveContainer.getQueue();
		while (waveQueue.Count > 0) {
			// Events.getInstance().waveBegan.Invoke(waveCount++);
			yield return new WaitForSeconds(waveBreak);
			yield return spawnEnemies(waveQueue.Dequeue());
		}
	}

	// Spawn enemies periodically every Wave.period seconds
	IEnumerator spawnEnemies(Wave wave) {
		Queue<Enemy> enemyQueue = wave.getEnemyQueue();

		while (enemyQueue.Count > 0) {
			Enemy enemyPrefab = enemyQueue.Dequeue();
			
			if (enemyPrefab is null)
				Debug.Log("Enemy null");
			
			objectPool.spawn(enemyPrefab.gameObject, transform.position);
			yield return new WaitForSeconds(wave.getPeriod());
		}
	}
}
