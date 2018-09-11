using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] float levelExitSlowMo = 0.2f;
    [SerializeField] AudioClip levelFinishSFX;

    private SFXplayer sfxplayer;

    private void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel() {
        sfxplayer = FindObjectOfType<SFXplayer>();
        sfxplayer.PlaySFX(levelFinishSFX);

        Time.timeScale = levelExitSlowMo;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
