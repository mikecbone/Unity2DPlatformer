using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {
    [SerializeField] AudioClip coinPickupSFX;
    private SFXplayer sfxplayer;

    private void Start() {
        sfxplayer = FindObjectOfType<SFXplayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        sfxplayer.PlaySFX(coinPickupSFX);
        Destroy(this.gameObject);
    }
}
