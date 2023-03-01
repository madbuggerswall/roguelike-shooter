using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy {
	protected override IEnumerator chaseAndAttack() {
		Transform target = LevelManager.getInstance().getHero().transform;

		float noticeDuration = 0.2f;
		float attackDuration = 0.5f;
		float attackDistance = 2f;

		// While player is alive/not beaten
		while (true) {
			yield return chase(target);
			yield return notice(noticeDuration);
			yield return melee(target, attackDuration, attackDistance);
		}
	}

	// Movement
	protected override IEnumerator chase(Transform target) {
		Coroutine directionRoutine = StartCoroutine(calculateDirectionPeriodically(target, 0.2f));
		yield return chaseUntilTargetInRadius(target);
		StopCoroutine(directionRoutine);
	}
}
