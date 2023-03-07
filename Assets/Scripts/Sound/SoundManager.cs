using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
	EnemySound enemySound;
	ProjectileSound projectileSound;
	CollectibleSound collectibleSound;

	void Awake() {
		enemySound = GetComponentInChildren<EnemySound>();
		projectileSound = GetComponentInChildren<ProjectileSound>();
		collectibleSound = GetComponentInChildren<CollectibleSound>();
	}

	public EnemySound getEnemySounds() { return enemySound; }
	public ProjectileSound getProjectileSound() { return projectileSound; }
	public CollectibleSound getCollectibleSound() { return collectibleSound; }
}
