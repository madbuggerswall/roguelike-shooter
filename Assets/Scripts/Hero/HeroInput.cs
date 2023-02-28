using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-4)]
public class HeroInput : MonoBehaviour {

	UnityAction controller;
	Vector2 input;

	void Awake() {
		initializeController();
	}

	void Update() {
		controller();
	}

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

	void mobileController() {
		float deltaMul = 2f;
		input.y += deltaMul * Time.deltaTime * Touchscreen.current.primaryTouch.delta.y.ReadValue();
		input.x += deltaMul * Time.deltaTime * Touchscreen.current.primaryTouch.delta.x.ReadValue();
		input.Normalize();

		if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended) {
			input = Vector2.zero;
		}
	}

	void initializeController() {
		bool isAndroid = Application.platform == RuntimePlatform.Android;
		bool isIOS = Application.platform == RuntimePlatform.IPhonePlayer;

		if (isAndroid || isIOS) {
			controller = mobileController;
		} else {
			controller = keyboardController;
		}
	}

	// Getters
	public Vector2 getInput() { return input; }
}
