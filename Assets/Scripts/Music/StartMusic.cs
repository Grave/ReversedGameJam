using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMusic : MonoBehaviour {

    [SerializeField] string musicTitle = "Default";

	// Use this for initialization
	void Start () {
        MusicController.Instance.PlayMusic(musicTitle);	
	}

}
