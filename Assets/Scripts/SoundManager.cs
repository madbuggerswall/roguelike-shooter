using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	[SerializeField] AudioSource playerHit;
	[SerializeField] AudioSource playerNoticed;
	[SerializeField] AudioSource enemyHit;
	[SerializeField] AudioSource projectileThrown;

	void Start() {
		Events.getInstance().playerHit.AddListener(delegate { playAudioSource(playerHit); });
		Events.getInstance().playerNoticed.AddListener(delegate { playAudioSource(playerNoticed); });
		Events.getInstance().enemyHit.AddListener(delegate { playAudioSource(enemyHit); });
		Events.getInstance().projectileThrown.AddListener(delegate { playAudioSource(projectileThrown); });
	}

	// Randomize pitch slightly in every play
	void playAudioSource(AudioSource audioSource) {
		audioSource.pitch = Random.Range(0.96f, 1.04f);
		audioSource.Play();
	}
}
