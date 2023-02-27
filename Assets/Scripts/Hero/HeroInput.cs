using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-4)]
public class HeroInput : MonoBehaviour {

	Vector2 input;

	void Update() {
		keyboardController();
	}

	// void OnMove(InputValue value) { moveDirection = value.Get<Vector2>(); }

	// Prototyping
	void keyboardController() {
		if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) {
			input.y = 1;
		} else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) {
			input.y = -1;
		} else {
			input.y = 0;
		}

		if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) {
			input.x = -1;
		} else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) {
			input.x = 1;
		} else {
			input.x = 0f;
		}
	}

	public Vector2 getInput() { return input; }
}
