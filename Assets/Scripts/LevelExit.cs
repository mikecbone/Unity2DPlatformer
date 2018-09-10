﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour {
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] float levelExitSlowMo = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision) {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel() {
        Time.timeScale = levelExitSlowMo;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentSceneIndex + 1);

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
