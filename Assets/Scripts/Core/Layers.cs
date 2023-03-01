using UnityEngine;

// Call initialize in LevelManager or GameManager
public class Layers {
	public static int wall;
	public static int enemy;
	public static int enemyProjectile;
	public static int projectile;
	public static int hero;
	public static int collectible;

	public static int wallMask;
	public static int enemyMask;
	public static int projectileMask;
	public static int enemyProjectileMask;
	public static int heroMask;
	public static int collectibleMask;

	public static void initialize() {
		wall = LayerMask.NameToLayer("Wall");
		enemy = LayerMask.NameToLayer("Enemy");
		enemyProjectile = LayerMask.NameToLayer("Enemy Projectile");
		projectile = LayerMask.NameToLayer("Projectile");
		hero = LayerMask.NameToLayer("Hero");
		collectible = LayerMask.NameToLayer("Collectible");

		wallMask = LayerMask.GetMask("Wall");
		enemyMask = LayerMask.GetMask("Enemy");
		enemyProjectileMask = LayerMask.GetMask("Enemy Projectile");
		projectileMask = LayerMask.GetMask("Projectile");
		heroMask = LayerMask.GetMask("Hero");
		collectibleMask = LayerMask.GetMask("Collectible");
	}
}