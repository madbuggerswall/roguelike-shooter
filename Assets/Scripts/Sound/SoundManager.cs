using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
	EnemySound enemySound;
	PlayerSound playerSound;
	ProjectileSound projectileSound;
	CollectibleSound collectibleSound;

	void Awake() {
		enemySound = GetComponentInChildren<EnemySound>();
		playerSound = GetComponentInChildren<PlayerSound>();
		projectileSound = GetComponentInChildren<ProjectileSound>();
		collectibleSound = GetComponentInChildren<CollectibleSound>();
	}

	public EnemySound getEnemySounds() { return enemySound; }
	public PlayerSound getPlayerSound() { return playerSound; }
	public ProjectileSound getProjectileSound() { return projectileSound; }
	public CollectibleSound getCollectibleSound() { return collectibleSound; }
}
