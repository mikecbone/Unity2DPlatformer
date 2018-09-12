using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour {
    [SerializeField] AudioClip heartPickupSFX;
    [SerializeField] int heartLives = 1;

    private SFXplayer sfxplayer;
    private bool addedToLives = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (addedToLives || GameSession.instance.CheckIfMaxLives()) {
            return;
        }
        addedToLives = true;
        sfxplayer = FindObjectOfType<SFXplayer>();
        sfxplayer.PlaySFX(heartPickupSFX);
        GameSession.instance.AddToLives(heartLives);
        Destroy(this.gameObject);
    }
}
