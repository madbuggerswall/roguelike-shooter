using UnityEngine;

// Call initialize in LevelManager or GameManager
public class Layers {
	public static int wall;
	public static int enemy;

	public static int wallMask;
	public static int enemyMask;

	public static void initialize() {
		wall = LayerMask.NameToLayer("Wall");
		enemy = LayerMask.NameToLayer("Enemy");

		wallMask = LayerMask.GetMask("Wall");
		enemyMask = LayerMask.GetMask("Enemy");
	}
}