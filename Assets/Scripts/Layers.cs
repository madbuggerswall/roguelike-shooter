using UnityEngine;

// Call initialize in LevelManager or GameManager
public class Layers {
	public static int wall;
	public static int enemy;
	public static int projectile;

	public static int wallMask;
	public static int enemyMask;
	public static int projectileMask;

	public static void initialize() {
		wall = LayerMask.NameToLayer("Wall");
		enemy = LayerMask.NameToLayer("Enemy");
		projectile = LayerMask.NameToLayer("Projectile");

		wallMask = LayerMask.GetMask("Wall");
		enemyMask = LayerMask.GetMask("Enemy");
		projectileMask = LayerMask.GetMask("Projectile");
	}
}