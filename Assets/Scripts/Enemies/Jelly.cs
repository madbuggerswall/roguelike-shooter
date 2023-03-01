using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : Enemy {
	// Behavior | Strategy
	protected override IEnumerator chaseAndAttack() {
		Transform target = LevelManager.getInstance().getHero().transform;

		float noticeDuration = 0.2f;
		float attackDuration = 0.5f;
		float attackDistance = 1f;

		// While player is alive/not beaten
		while (true) {
			yield return chase(target);
			yield return notice(noticeDuration);
			yield return melee(target, attackDuration, attackDistance);
		}
	}
}