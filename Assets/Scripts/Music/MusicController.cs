using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : JamUtilities.MonoSingleton<MusicController> {
    [SerializeField] private PlaySound[] musics = { };
    [SerializeField] private string currentMusic;

    public void PlayMusic(string key) {
        if (key == currentMusic) {
            return;
        }

        if (musics.Length == 0) {
            return;
        }

        foreach (var music in musics) {
            if (key == music.key) {
                Play(music);
                return;
            }
        }
    }

    private void Play(PlaySound nextMusic) {
        currentMusic = nextMusic.key;
        AudioSource source = GetComponent<AudioSource>();
        source.Stop();
        source.clip = nextMusic.clip;
        source.Play();
    }
}
