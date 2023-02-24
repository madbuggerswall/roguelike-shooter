using UnityEngine;

// Call initialize in LevelManager or GameManager
public class Layers {
	public static int wall;
	public static int enemy;
	public static int projectile;
	public static int weapon;
	public static int player;
	public static int collectible;

	public static int wallMask;
	public static int enemyMask;
	public static int projectileMask;
	public static int weaponMask;
	public static int playerMask;
	public static int collectibleMask;

	public static void initialize() {
		wall = LayerMask.NameToLayer("Wall");
		enemy = LayerMask.NameToLayer("Enemy");
		projectile = LayerMask.NameToLayer("Projectile");
		weapon = LayerMask.NameToLayer("Weapon");
		player = LayerMask.NameToLayer("Player");
		collectible = LayerMask.NameToLayer("Collectible");

		wallMask = LayerMask.GetMask("Wall");
		enemyMask = LayerMask.GetMask("Enemy");
		projectileMask = LayerMask.GetMask("Projectile");
		weaponMask = LayerMask.GetMask("Weapon");
		playerMask = LayerMask.GetMask("Player");
		collectibleMask = LayerMask.GetMask("Collectible");
	}
}