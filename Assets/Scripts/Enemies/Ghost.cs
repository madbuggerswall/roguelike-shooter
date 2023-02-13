using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {
	protected override EnemyType getEnemyType() { return EnemyType.ghost; }
}
