using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinScore = 1;

    private SFXplayer sfxplayer;
    private bool addedToScore = false;

    private void Start() {
        sfxplayer = FindObjectOfType<SFXplayer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (addedToScore) {
            return;
        }
        addedToScore = true;
        sfxplayer.PlaySFX(coinPickupSFX);
        FindObjectOfType<GameSession>().AddToScore(coinScore);
        Destroy(this.gameObject);
    }
}
