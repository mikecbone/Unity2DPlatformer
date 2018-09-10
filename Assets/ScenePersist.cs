using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {
    static ScenePersist instance = null;
    int startingSceneIndex;

    // Use this for initialization
    void Start () {
        if (!instance) {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this) {
            Destroy(this.gameObject);
        }
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (startingSceneIndex != SceneManager.GetActiveScene().buildIndex) {
            instance = null;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }
}
