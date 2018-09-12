using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXplayer : MonoBehaviour {
    private AudioSource audioSource;

	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	public void PlaySFX(AudioClip sfx) {
        audioSource.PlayOneShot(sfx);
    }
}
