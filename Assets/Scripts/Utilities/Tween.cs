using UnityEngine;

public static class Tween {

	// Quadratic easing in - accelerating from zero velocity
	public static float easeInQuad(float elapsedTime, float duration) {
		elapsedTime /= duration;
		return elapsedTime * elapsedTime;
	}

	// Quadratic easing out - decelerating to zero velocity
	public static float easeOutQuad(float elapsedTime, float duration) {
		elapsedTime /= duration;
		return -elapsedTime * (elapsedTime - 2);
	}

	//  Quadratic easing in/out - acceleration until halfway, then deceleration
	public static float easeInOutQuad(float elapsedTime, float duration) {
		elapsedTime /= duration / 2;

		if (elapsedTime < 1)
			return 1 / 2 * elapsedTime * elapsedTime;

		elapsedTime--;
		return -1 / 2 * (elapsedTime * (elapsedTime - 2) - 1);
	}

	// Cubic easing in - accelerating from zero velocity
	public static float easeInCubic(float elapsedTime, float duration) {
		elapsedTime /= duration;
		return elapsedTime * elapsedTime * elapsedTime;
	}

	// Cubic easing out - decelerating to zero velocity
	public static float easeOutCubic(float elapsedTime, float duration) {
		elapsedTime /= duration;
		elapsedTime--;
		return elapsedTime * elapsedTime * elapsedTime + 1;
	}


	// Cubic easing in/out - acceleration until halfway, then deceleration
	public static float easeInOutCubic(float elapsedTime, float duration) {
		elapsedTime /= duration / 2;

		if (elapsedTime < 1)
			return 1 / 2 * elapsedTime * elapsedTime * elapsedTime;

		elapsedTime -= 2;
		return 1 / 2 * (elapsedTime * elapsedTime * elapsedTime + 2);
	}

	// Quartic easing in - accelerating from zero velocity
	public static float easeInQuart(float elapsedTime, float duration) {
		elapsedTime /= duration;
		return elapsedTime * elapsedTime * elapsedTime * elapsedTime;
	}

	// Quartic easing out - decelerating to zero velocity
	public static float easeOutQuart(float elapsedTime, float duration) {
		elapsedTime /= duration;
		elapsedTime--;
		return -(elapsedTime * elapsedTime * elapsedTime * elapsedTime - 1);
	}

	// Quartic easing in/out - acceleration until halfway, then deceleration
	public static float easeInOutQuart(float elapsedTime, float duration) {
		elapsedTime /= duration / 2;

		if (elapsedTime < 1)
			return 1 / 2 * elapsedTime * elapsedTime * elapsedTime * elapsedTime;

		elapsedTime -= 2;
		return -1 / 2 * (elapsedTime * elapsedTime * elapsedTime * elapsedTime - 2);
	}

	// Quintic easing in - accelerating from zero velocity
	public static float easeInQuint(float elapsedTime, float duration) {
		elapsedTime /= duration;
		return elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime;
	}

	// Quintic easing out - decelerating to zero velocity
	public static float easeOutQuint(float elapsedTime, float duration) {
		elapsedTime /= duration;
		elapsedTime--;
		return (elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + 1);
	}

	// Quintic easing in/out - acceleration until halfway, then deceleration
	public static float easeInOutQuint(float elapsedTime, float duration) {
		elapsedTime /= duration / 2;

		if (elapsedTime < 1)
			return 1 / 2 * elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime;

		elapsedTime -= 2;
		return 1 / 2 * (elapsedTime * elapsedTime * elapsedTime * elapsedTime * elapsedTime + 2);
	}

	// Sinusoidal easing in - accelerating from zero velocity
	public static float easeInSine(float elapsedTime, float duration) {
		return 1 - Mathf.Cos(elapsedTime / duration * (Mathf.PI / 2));
	}

	// Sinusoidal easing out - decelerating to zero velocity
	public static float easeOutSine(float elapsedTime, float duration) {
		return Mathf.Sin(elapsedTime / duration * (Mathf.PI / 2));
	}

	// Sinusoidal easing in/out - accelerating until halfway, then decelerating
	public static float easeInOutSine(float elapsedTime, float duration) {
		return -1 / 2 * (Mathf.Cos(Mathf.PI * elapsedTime / duration) - 1);
	}

	// Exponential easing in - accelerating from zero velocity
	public static float easeInExpo(float elapsedTime, float duration) {
		return Mathf.Pow(2, 10 * (elapsedTime / duration - 1));
	}

	// Exponential easing out - decelerating to zero velocity
	public static float easeOutExpo(float elapsedTime, float duration) {
		return (-Mathf.Pow(2, -10 * elapsedTime / duration) + 1);
	}

	// Exponential easing in/out - accelerating until halfway, then decelerating
	public static float easeInOutExpo(float elapsedTime, float duration) {
		elapsedTime /= duration / 2;

		if (elapsedTime < 1)
			return 1 / 2 * Mathf.Pow(2, 10 * (elapsedTime - 1));

		elapsedTime--;
		return 1 / 2 * (-Mathf.Pow(2, -10 * elapsedTime) + 2);
	}

	// Circular easing in - accelerating from zero velocity
	public static float easeInCirc(float elapsedTime, float duration) {
		elapsedTime /= duration;
		return -(Mathf.Sqrt(1 - elapsedTime * elapsedTime) - 1);
	}

	// Circular easing out - decelerating to zero velocity
	public static float easeOutCirc(float elapsedTime, float duration) {
		elapsedTime /= duration;
		elapsedTime--;
		return Mathf.Sqrt(1 - elapsedTime * elapsedTime);
	}

	// Circular easing in/out - acceleration until halfway, then deceleration
	public static float easeInOutCirc(float elapsedTime, float duration) {
		elapsedTime /= duration / 2;

		if (elapsedTime < 1)
			return -1 / 2 * (Mathf.Sqrt(1 - elapsedTime * elapsedTime) - 1);

		elapsedTime -= 2;
		return 1 / 2 * (Mathf.Sqrt(1 - elapsedTime * elapsedTime) + 1);
	}
}