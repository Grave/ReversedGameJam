using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    [SerializeField] private PlaySound[] sounds;
    private AudioSource audioSource;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(string key) {
		foreach (var sound in sounds)
        {
            if (key == sound.key) {
                StopSound();
                Play(sound.clip);
                return;
            }
        }
	}

    private void PlaySoundNoStop(string key) {
        foreach (var sound in sounds) {
            if (key == sound.key) {
                Play(sound.clip);
                return;
            }
        }
    }

    private void PlaySoundLoop(string key) {
        foreach (var sound in sounds) {
            if (key == sound.key) {
                PlayLoop(sound.clip);
                return;
            }
        }
    }

    private void StopSound() {
        audioSource.Stop();
    }

    private void Play(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    private void PlayLoop(AudioClip clip) {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
