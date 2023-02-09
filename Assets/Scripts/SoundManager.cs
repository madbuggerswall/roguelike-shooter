using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	[SerializeField] AudioClip bruh;

	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	void Start() {
		Events.getInstance().playerHit.AddListener(delegate { playAudioSource(bruh); });
	}

	// Randomize pitch slightly in every play
	void playAudioSource(AudioClip audioClip) {
		audioSource.clip = audioClip;
		audioSource.pitch = Random.Range(0.96f, 1.04f);
		audioSource.Play();
	}
}
