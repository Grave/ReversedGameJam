using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    [SerializeField] private PlaySound[] sounds;

	private void PlaySound(string key) {
		foreach (var sound in sounds)
        {
            if (key == sound.key) {
                Play(sound.clip);
                return;
            }
        }
	}

    private void Play(AudioClip clip) {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }
}
